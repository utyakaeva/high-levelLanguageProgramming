using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApplication6.Data;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        //прокат, который выбран в данный момент
        Rent currentRent;
        //список книг выбранного проката
        List<Book> currentBooks = new List<Book>();
        //есть ли изменения на форме
        bool isModified;
        bool isEditing;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //отключение автогенерации колонок в таблицах
            grdBooks.AutoGenerateColumns = false;
            grdRents.AutoGenerateColumns = false;
            grdBooks2.AutoGenerateColumns = false;
            grdReaders.AutoGenerateColumns = false;
            grdLibrarians.AutoGenerateColumns = false;

            cmbReaders.DataSource = readerBindingSource; //заполнение выпадающего списка читателей
            cmbLibrarians.DataSource = librarianBindingSource; //заполнение выпадающего списка библиотекарей            
            rentBindingSource.DataSource = Rent.Data; //заполнение списка прокатов
            bookBindingSource.DataSource = Book.Data; //заполнение списка книг
            readerBindingSource.DataSource = Reader.Data; //заполнение списка читателей
            librarianBindingSource.DataSource = Librarian.Data; //заполнение списка читателей

            SelectFirstRent(); //выбрать первый прокат (или создать, если список прокатов пустой)
        }

        void UpdateHeader()
        {
            if (Settings.Instance.ShowNumberInHeader)
                tabPage1.Text = $"Прокат №{currentRent.Id}";
            else
                tabPage1.Text = "Прокат";
        }

        //Обновить все поля на форме, т.е. показать информацию о текущем прокате.
        //Применяется при запуске программы, или при переходе на другой прокат
        void UpdateForm()
        {
            UpdateHeader();

            var selectedReader = Reader.Data.Find(r => r.Id == currentRent.ReaderId);
            cmbReaders.SelectedItem = selectedReader;

            var selectedLibrarian = Librarian.Data.Find(r => r.Id == currentRent.LibrarianId);
            cmbLibrarians.SelectedItem = selectedLibrarian;

            rbtGuarateeed1.Checked = currentRent.Guaranteed;
            rbtGuarateeed2.Checked = !currentRent.Guaranteed;

            cbIsFinished.Checked = currentRent.IsFinished;

            dtpCreateDate.Value = currentRent.CreateDate;

            tbGuaranteeSum.Text = currentRent.GuaranteeSum;

            tbComment.Text = currentRent.Comment?.Replace("<newline>", "\r\n").Replace("<comma>", ",");

            currentBooks = Book.Data.FindAll(t => currentRent.BookIds.Contains(t.IndividualNumber));
            UpdateBooksSelector();
            UpdateBooksGrid();
            UpdateReturnDate();

            isModified = false;
            isEditing = false;
            UpdateElementsState();
        }

        //Обновить выпадающий список книг на форме
        void UpdateBooksSelector()
        {
            //сначала мы создадим список с идентификаторами всех книг, которые уже выданы в других прокатах
            //для этого сделаем перебор по всем прокатам, которые не сданы, кроме текущего
            var missingBookIds = new List<int>();
            foreach (var rent in Rent.VisibleRents)
            {
                if (!rent.IsFinished && rent.Id != currentRent.Id)
                    missingBookIds.AddRange(rent.BookIds);
            }

            //Теперь выберем все книги, которые отсутствуют в списке уже выданных книг в других прокатах, 
            //а также отсутствуют в списке книг, которые уже выданы в текущем прокате
            var books = Book.Data.FindAll(t => !missingBookIds.Contains(t.IndividualNumber) && currentBooks.Find(t2 => t2.IndividualNumber == t.IndividualNumber) == null);

            //Добавим найденные книги в выпадающий список
            cmbBooks.Items.Clear();
            cmbBooks.Items.AddRange(books.ToArray());
        }

        //обновить состояние всех кнопок и полей
        //В этой функции делаем доступными или недоступными кнопки в зависимости от заданных условий        
        void UpdateElementsState()
        {
            int index = Rent.VisibleRents.IndexOf(currentRent);
            //Если выбрана первая книга в списке, то кнопка "назад" недоступна
            btPrev.Enabled = !isEditing && index > 0;
            //Если выбрана последняя книга в списке, то кнопка "вперед" недоступна
            btNext.Enabled = !isEditing && index + 1 < Rent.VisibleRents.Count;

            grdBooks.Enabled = isEditing;
            btDelete.Enabled = !isEditing;
            btGiveBook.Enabled = !isEditing;

            //сумма залога не указывается, если отмечено что прокат без залога
            tbGuaranteeSum.Enabled = isEditing && rbtGuarateeed1.Checked && !cbIsFinished.Checked;
            //кнопка "добавить книгу" недоступна, если книга не выбрана
            btAddBook.Enabled = isEditing && !cbIsFinished.Checked && cmbBooks.SelectedItem is Book;

            //кнопка "сохранить" недоступна, если на форме не были внесены изменения
            btSave.Enabled = isEditing && isModified;

            //Остальные поля и кнопки недоступны, только если прокат помечен как сданный
            rbtGuarateeed1.Enabled = isEditing && !cbIsFinished.Checked;
            rbtGuarateeed2.Enabled = isEditing && !cbIsFinished.Checked;
            tbComment.Enabled = isEditing && !cbIsFinished.Checked;
            btGiveBook.Enabled = isEditing && !cbIsFinished.Checked;
            dtpCreateDate.Enabled = isEditing && !cbIsFinished.Checked;
            cmbBooks.Enabled = isEditing && !cbIsFinished.Checked;
            cmbLibrarians.Enabled = isEditing && !cbIsFinished.Checked;
            cmbReaders.Enabled = isEditing && !cbIsFinished.Checked;

            btEdit.Enabled = isEditing || !cbIsFinished.Checked;
            btEdit.Text = isEditing ? "Отменить" : "Редактировать";
        }

        //Вычислить и обновить дату возврата. Эта функция вызывается, когда изменяется дата создания проката, либо когда изменяется список книг в прокате
        void UpdateReturnDate()
        {
            var daysToAdd = 3;
            if (currentBooks.Exists(t => t.nomer == 1))
                daysToAdd = 10;
            if (currentBooks.Exists(nomer => nomer.nomer == 0))
                daysToAdd = 13;
            //if (currentBooks.Exists(nomer => nomer.nomer == 0))
            //if (currentBooks.Exists(t => t.nomer == 1))

            dtpReturnDate.Value = dtpCreateDate.Value.AddDays(daysToAdd);
        }

        //обновить список книг, выданных в прокат. Вызывается при переходе на новый прокат, а также при каждом изменении списка (добавление или удаление книги)
        void UpdateBooksGrid()
        {
            //if (grdBooks.DataSource == currentBooks)
            //    grdBooks.DataSource = new List<Book>();
            //grdBooks.DataSource = currentBooks;
            grdBooks.DataSource = new List<Book>(currentBooks);
        }

        //Сохранение данных. В этой функции мы сначала заполняем объект currentRent данными, которые ввел пользователь на форме, 
        //а затем вызываем метод Rent.Save, который записывает изменения в файл прокатов
        private void btSave_Click(object sender, EventArgs e)
        {
            //проверка правильности заполнения формы
            var error = GetValidationError();
            if (!string.IsNullOrEmpty(error))
            {
                toolStripStatusLabel1.Text = $"Введены неверные данные. {error}";
                return;
            }

            currentRent.ReaderId = (cmbReaders.SelectedItem as Reader)?.Id;

            currentRent.LibrarianId = (cmbLibrarians.SelectedItem as Librarian)?.Id;

            currentRent.Guaranteed = rbtGuarateeed1.Checked;

            currentRent.IsFinished = cbIsFinished.Checked;

            currentRent.CreateDate = dtpCreateDate.Value;

            currentRent.GuaranteeSum = currentRent.Guaranteed ? tbGuaranteeSum.Text : "";

            //В комментарии символы "новая строка" и "," заменим на текстовое представление (<comma> и <newline>)
            //иначе не сможем его прочитать
            currentRent.Comment = tbComment.Text.Replace(",", "<comma>").Replace("\r\n", "<newline>").Replace("\n", "<newline>");
            if (currentRent.Comment.Length > 1000)
            {
                MessageBox.Show("Размер комментария будет обрезан до 1000 символов.");
                currentRent.Comment = currentRent.Comment.Substring(0, 1000);
            }

            //Конвертируем список книг проката (List<Book>) в список идентификаторов этих книг (List<int>)
            currentRent.BookIds = currentBooks.ConvertAll(t => t.IndividualNumber);

            //сохраняем
            Rent.Save();

            //сбрасываем изменения и обновляем состояние кнопок на странице (кнопка "сохранить" должна стать недоступной)
            isModified = false;
            isEditing = false;
            UpdateElementsState();

            toolStripStatusLabel1.Text = "Успешно сохранено.";
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            //создание нового проката.  Если есть какие-то изменения предложим их сохранить
            if (isModified)
            {
                if (MessageBox.Show("Если перейти на другую запись, несохраненные изменения пропадут. Продолжить?", "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }

            //Добавим новый прокат в конец списка, выберем его и вызовем функцию полного обновления формы
            Rent.Data.Add(new Rent());
            RefreshRents();
            Rent.Save();
            currentRent = Rent.VisibleRents.Last();
            currentRent.LibrarianId = Settings.Instance.DefaultLibrarianId;
            UpdateForm();
            btEdit.PerformClick();
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            //переход на предыдущий прокат
            if (isModified)
            {
                if (MessageBox.Show("Если перейти на другую запись, несохраненные изменения пропадут. Продолжить?", "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }

            //Вычислим номер текущего проката в списке, уменьшим его на 1, выберем элемент с полученным индексом и вызовем функцию полного обновления формы
            int index = Rent.VisibleRents.IndexOf(currentRent);
            index--;
            if (index >= 0)
                currentRent = Rent.VisibleRents[index];
            UpdateForm();
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            //переход на следующий прокат.
            if (isModified)
            {
                if (MessageBox.Show("Если перейти на другую запись, несохраненные изменения пропадут. Продолжить?", "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    return;
            }

            int index = Rent.VisibleRents.IndexOf(currentRent);
            index++;
            if (index < Rent.VisibleRents.Count)
            {
                currentRent = Rent.VisibleRents[index];
            }
            UpdateForm();
        }

        //Добавление книги в прокат
        private void btAddBook_Click(object sender, EventArgs e)
        {
            isModified = true;

            //Получим объект книги, который выбрал пользователь
            var book = cmbBooks.SelectedItem as Book;
            //Если такая книга еще отсутствует в списке, добавим ее
            if (book != null && currentBooks.Find(t => t.IndividualNumber == book.IndividualNumber) == null)
                currentBooks.Add(book);

            //Полное обновление всех данных на форме
            UpdateBooksSelector();
            UpdateBooksGrid();
            UpdateElementsState();
            UpdateReturnDate();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            //удаление информации о текущем прокате. Спрашиваем у пользователя подтверждение
            if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Удаляем информацию, сохраняем список прокатов в файл и переходим к первой записи 
                //(либо создаем ее, если после удаления больше не осталось ни одного проката)
                currentRent.IsDeleted = true;
                RefreshRents();
                Rent.Save();
                SelectFirstRent();

                toolStripStatusLabel1.Text = "Данные удалены.";
            }
        }

        //Переходим к первой записи о прокате, либо создаем ее, если список прокатов пустой
        void SelectFirstRent()
        {
            if (Rent.VisibleRents.Count <= 0)
            {
                Rent.Data.Add(new Rent());
                RefreshRents();
            }
            currentRent = Rent.VisibleRents[0];
            UpdateForm();
        }

        //эта функция вызывается при каждом нажатии на таблицу с книгами
        private void grdBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            //Если была нажата кнопка "удалить"
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                //установим изменения
                isModified = true;

                //удалим книгу из списка 
                currentBooks.RemoveAt(e.RowIndex);

                //обновление выпадающего списка книг, таблицы книг, состояния кнопок и полей, даты возврата
                UpdateBooksSelector();
                UpdateBooksGrid();
                UpdateElementsState();
                UpdateReturnDate();
            }
        }

        //эта функция вызывается, когда пользователь изменяет состояние кнопок (с залогом или без залога)
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            if (sender == rbtGuarateeed1)
                isModified = true;
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь изменяет дату создания проката
        private void dtpCreateDate_ValueChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок и дату возврата
            isModified = true;
            UpdateReturnDate();
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь изменяет состояние галочки "прокат сдан"
        private void cbIsFinished_CheckedChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            isModified = true;
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь выбирает книгу в выпадающем списке и нужно сделать активной кнопку "добавить"
        private void cmbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //обновим состояние кнопок
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь изменяет сумму залога
        private void btGuaranteeSum_TextChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            isModified = true;
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь выбирает другое значение в списке читателей
        private void cmbReaders_SelectedValueChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            isModified = true;
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь выбирает другое значение в списке библиотекарей
        private void cmbLibrarians_SelectedValueChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            isModified = true;
            UpdateElementsState();
        }

        //эта функция вызывается, когда пользователь изменяет текст комментария
        private void tbComment_TextChanged(object sender, EventArgs e)
        {
            //установим изменения и обновим состояние кнопок
            isModified = true;
            UpdateElementsState();
        }

        //Возвращает ошибку заполнения формы (если есть)
        string GetValidationError()
        {
            if (tbGuaranteeSum.Enabled)
            {
                int guaranteeSum;
                if (!int.TryParse(tbGuaranteeSum.Text, out guaranteeSum))
                {
                    return "В поле \"Залог\" должно быть указано число.";
                }
            }

            if (cmbLibrarians.SelectedItem == null)
                return "Не указан библиотекарь.";

            if (cmbReaders.SelectedItem == null)
                return "Не указан читатель.";

            if (currentBooks.Count <= 0)
                return "Список книг пуст.";

            return null;
        }

        //эта функция вызывается, когда пользователь нажимает на кнопку "Сдать книгу"
        private void btGiveBook_Click(object sender, EventArgs e)
        {
            //проверка правильности заполнения формы
            var error = GetValidationError();
            if (!string.IsNullOrEmpty(error))
            {
                toolStripStatusLabel1.Text = $"Введены неверные данные. {error}";
                return;
            }

            //посчитаем срок просрочки
            var delay = DateTime.Now - dtpReturnDate.Value;

            //посчитаем задолженность
            var debt = 0d;
            if (tbGuaranteeSum.Enabled)
            {
                //Разбираем число, указанное в поле "залог"
                int guaranteeSum = int.Parse(tbGuaranteeSum.Text);
                //и вычисляем задолженность по формуле
                debt = guaranteeSum * (int)delay.TotalDays * 0.1;
            }

            //сформируем и покажем пользователю сообщение с вычисленными данными
            string delayMsg = delay.TotalDays > 0 ? $"Просрочено на {(int)delay.TotalDays} дней" : "Пока не просрочено.";
            string debtMsg = delay.TotalDays > 0 ? $"Задолженность: {debt}" : "Задолженности нет.";
            MessageBox.Show(delayMsg + "\r\n" + debtMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cbIsFinished.Checked = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified && isEditing)
            {
                if (MessageBox.Show("Если закрыть приложение, несохраненные изменения пропадут. Продолжить?", "",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    e.Cancel = true;
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (currentRent.IsFinished)
            {
                MessageBox.Show("Нельзя редактировать уже завершенный прокат.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (isEditing)
            {
                isEditing = false;
                UpdateForm();
            } else
            {
                isEditing = true;
                UpdateElementsState();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new FormOptions().ShowDialog() == DialogResult.OK)
            {
                UpdateHeader();
                toolStripStatusLabel1.Text = "Настройки успешно сохранены.";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void RefreshBooks()
        {
            bookBindingSource.DataSource = new List<Book>(Book.Data);
        }

        void RefreshRents()
        {
            rentBindingSource.DataSource = new List<Rent>(Rent.Data);
        }

        void RefreshReaders()
        {
            readerBindingSource.DataSource = new List<Reader>(Reader.Data);
        }

        void RefreshLibrarians()
        {
            librarianBindingSource.DataSource = new List<Librarian>(Librarian.Data);
        }

        private void grdRents_DoubleClick(object sender, EventArgs e)
        {
            if (grdRents.SelectedRows.Count > 0)
            {
                if (currentRent != grdRents.SelectedRows[0].DataBoundItem)
                {
                    if (isModified)
                    {
                        if (MessageBox.Show("Если перейти на другую запись, несохраненные изменения пропадут. Продолжить?", "",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                            return;
                    }

                    currentRent = (Rent)grdRents.SelectedRows[0].DataBoundItem;
                    UpdateForm();
                }
                tabControl1.SelectedIndex = 0;
                btEdit_Click(null, null);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btNew.PerformClick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Settings.Save();
            Rent.Save();
            toolStripStatusLabel1.Text = $"Данные сохранены по таймеру ({DateTime.Now.ToShortTimeString()})";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            this.Activate();
        }

        private void contextAdd_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                var entity = new Book();
                var form = new FormBook(entity);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Book.Data.Add(entity);
                    Book.Save();
                    RefreshBooks();
                    UpdateForm();
                }
            } else if (tabControl1.SelectedIndex == 3)
            {
                var entity = new Reader();
                var form = new FormReader(entity);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Reader.Data.Add(entity);
                    Reader.Save();
                    RefreshReaders();
                    UpdateForm();
                }
            } else if (tabControl1.SelectedIndex == 4)
            {
                var entity = new Librarian();
                var form = new FormLibrarian(entity);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Librarian.Data.Add(entity);
                    Librarian.Save();
                    RefreshLibrarians();
                    UpdateForm();
                }
            }
        }

        private void contextEdit_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                var entity = grdBooks2.SelectedRows.Count > 0 ? grdBooks2.SelectedRows[0].DataBoundItem as Book : null;
                if (entity == null)
                    MessageBox.Show("Книга для редактирования не выбрана");
                else
                {
                    var form = new FormBook(entity);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Book.Save();
                        RefreshBooks();
                        SelectFirstRent();
                    }
                }
            } else if (tabControl1.SelectedIndex == 3)
            {
                var entity = grdReaders.SelectedRows.Count > 0 ? grdReaders.SelectedRows[0].DataBoundItem as Reader : null;
                if (entity == null)
                    MessageBox.Show("Читатель для редактирования не выбран");
                else
                {
                    var form = new FormReader(entity);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Reader.Save();
                        RefreshReaders();
                        SelectFirstRent();
                    }
                }
            } else if (tabControl1.SelectedIndex == 4)
            {
                var entity = grdLibrarians.SelectedRows.Count > 0 ? grdLibrarians.SelectedRows[0].DataBoundItem as Librarian : null;
                if (entity == null)
                    MessageBox.Show("Библиотекарь для редактирования не выбран");
                else
                {
                    var form = new FormLibrarian(entity);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Librarian.Save();
                        RefreshLibrarians();
                        SelectFirstRent();
                    }
                }
            }
        }

        private void contextRemove_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                var entity = grdBooks2.SelectedRows.Count > 0 ? grdBooks2.SelectedRows[0].DataBoundItem as Book : null;
                if (entity == null)
                    MessageBox.Show("Книга для удаления не выбрана");
                else
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Book.Data.RemoveAll(t => t.IndividualNumber == entity.IndividualNumber);
                        Book.Save();
                        RefreshBooks();
                        SelectFirstRent();
                    }
                }
            } else if (tabControl1.SelectedIndex == 3)
            {
                var entity = grdReaders.SelectedRows.Count > 0 ? grdReaders.SelectedRows[0].DataBoundItem as Reader : null;
                if (entity == null)
                    MessageBox.Show("Читатель для удаления не выбран");
                else
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Reader.Data.RemoveAll(t => t.Id == entity.Id);
                        Reader.Save();
                        RefreshReaders();
                        SelectFirstRent();
                    }
                }
            } else if (tabControl1.SelectedIndex == 4)
            {
                var entity = grdLibrarians.SelectedRows.Count > 0 ? grdLibrarians.SelectedRows[0].DataBoundItem as Librarian : null;
                if (entity == null)
                    MessageBox.Show("Библиотекарь для удаления не выбран");
                else
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Librarian.Data.RemoveAll(t => t.Id == entity.Id);
                        Librarian.Save();
                        RefreshLibrarians();
                        SelectFirstRent();
                    }
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (isEditing)
            {
                MessageBox.Show("Для переключения на другие вкладки нужно выйти из режима редактирования.");
                e.Cancel = true;
            } else
            {
                if (e.TabPage == tabPage6)
                {
                    UpdateChart();
                }
            }
        }

        void UpdateChart()
        {
            chart1.Series[0].Points.Clear();

            var minDate = DateTime.Now.Date.AddMonths(-1);
            var date = DateTime.Now.Date;

            chart1.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = date.ToOADate();

            int max = 0;
            while (date >= minDate)
            {
                var rentsCount = Rent.Data.Count(r => r.CreateDate.Date == date.Date);
                chart1.Series[0].Points.AddXY(date, rentsCount);
                max = Math.Max(max, rentsCount);
                date = date.AddDays(-1);
            }
        }
    }
}