using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace EntityFramWorkCotrolBindigTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (Model1 db = new Model1())
            {
                this.bindingSource1.DataSource = db.EmailAddress.ToList();
                this.dataGridView1.DataSource = bindingSource1;
                this.bindingNavigator1.BindingSource = this.bindingSource1;
                this.textBox1.DataBindings.Add("Text", this.bindingSource1, "EmailAddress1", false);
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            using (Model1 db = new Model1())
            {
                //db.Database.Log = Console.WriteLine;
                //var query = db.EmailAddress.Where(a => SqlFunctions.PatIndex("%[" + searchTb.Text + "]%", a.EmailAddress1) > 0);
                var query = db.EmailAddress.Where(a => a.EmailAddress1.Contains(searchTb.Text)).ToList();
                this.bindingSource1.DataSource = query;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            int currentId = (int)dataGridView1.CurrentRow.Cells[0].Value;

            using (var db = new Model1())
            {
                try
                {
                    var item = db.EmailAddress.Single(x => x.EmailAddressID == currentId);
                    if (item != null)
                    {
                        db.EmailAddress.Remove(item);
                        string result = string.Format("{0}件更新", db.SaveChanges());
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    // 例外処理
                    ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList()
                        .ForEach(item => Console.WriteLine("{0} - {1}", item.PropertyName, item.ErrorMessage));
                }
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (Model1 db = new Model1())
            {
                var deleted = db.ChangeTracker.Entries<Address>()
                    .Where(x => x.State.HasFlag(EntityState.Deleted))
                    .ToList();
                this.bindingSource1.DataSource = deleted.ToList();
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("削除してもいいですか？", "確認", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            int currentRow = 0;
            //DataGridViewの場合
            currentRow = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string updateCount = "";
            using (var db = new Model1())
            {
                try
                {
                    var item = db.EmailAddress.SingleOrDefault(x => x.EmailAddressID == currentRow);
                    if (item != null)
                    {
                        db.EmailAddress.Remove(item);
                        updateCount = string.Format("{0}件削除", db.SaveChanges());
                        Console.WriteLine("{0}件削除", updateCount);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    // 例外処理
                    ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList()
                        .ForEach(item => Console.WriteLine("{0} - {1}", item.PropertyName, item.ErrorMessage));
                }
            }
            //DataGridViewの場合 表示上の削除
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }
    }
}
