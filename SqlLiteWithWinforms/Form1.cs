using DevExpress.XtraEditors;
using SqlLiteWithWinforms.Data;
using SqlLiteWithWinforms.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqlLiteWithWinforms
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        Proceder Entity { get; set; }

        void reSet()
        {
            Entity = null;
            NameTB.Text = string.Empty;
            SqlCodeTB.Text = string.Empty;
        }
        bool isValid()
        {
            bool isValid = false;
            isValid = !string.IsNullOrWhiteSpace(NameTB.Text);
            isValid = !string.IsNullOrWhiteSpace(SqlCodeTB.Text);
            if(!isValid)
                XtraMessageBox.Show("Not Valid Data");
            return isValid;
        }

        void add()
        {
            if (isValid())
            {
                try
                {
                    using (var db = new ApplicationDbContext())
                    {
                        db.Proceders.Add(new Proceder
                        {
                            Name = NameTB.Text,
                            SqlCode = SqlCodeTB.Text
                        });
                        db.SaveChanges();
                        reSet();
                        getAll();
                        XtraMessageBox.Show("Added");
                    }
                }
                catch(Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }

        void update()
        {
            if (isValid())
            {
                try
                {
                    using (var db = new ApplicationDbContext())
                    {
                        Entity.Name = NameTB.Text;
                        Entity.SqlCode = SqlCodeTB.Text;
                        db.Entry(Entity).State = EntityState.Modified;
                        db.SaveChanges();
                        reSet();
                        getAll();
                        XtraMessageBox.Show("Updated");
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }
        void View()
        {
            var index = gridView1.GetSelectedRows()[0];
            var row = (Proceder)gridView1.GetRow(index);
            Entity = row;
            NameTB.Text = Entity.Name;
            SqlCodeTB.Text = Entity.SqlCode;
        }
        void delete()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var index = gridView1.GetSelectedRows()[0];
                    var row = (Proceder)gridView1.GetRow(index);
                    //var entity = db.Proceders.Find(row.Id);
                    db.Entry(row).State = EntityState.Deleted;
                    db.SaveChanges();
                    var entities = (List<Proceder>)gridView1.DataSource;
                    entities.Remove(row);            
                    gridControl1.DataSource = entities;
                    gridControl1.RefreshDataSource();
                    XtraMessageBox.Show("Deleted");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        void getAll()
        {

            using (var db = new ApplicationDbContext())
            {
                gridControl1.DataSource = db.Proceders.ToList();
            }
        }
        public Form1()
        {
            InitializeComponent();
            getAll();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Entity == null)
                add();
            else
                update();
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            reSet();
        }

        private void colViewBtn_Click(object sender, EventArgs e)
        {
            View();
        }

        private void colDeleteBtn_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to delete?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                delete();
        }
    }
}
