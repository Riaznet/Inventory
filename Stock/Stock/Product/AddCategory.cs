using Common;
using Domain.Product;
using Repository.Product;
using System;
using System.Data;
using System.Windows.Forms;

namespace Stock.Stock.Product
{
    public partial class AddCategory : Form
    {
        ChildCategoryRepo cCatRepository;
        ParentCategoryRepo pCatRepository;
        ChildCategory cCat;
        ParentCategory pCat;
        //ChildCategory cCat;
        public AddCategory()
        {
            cCatRepository = new ChildCategoryRepo();
            pCatRepository = new ParentCategoryRepo();
            cCat = new ChildCategory();
            pCat = new ParentCategory();
            InitializeComponent();
            BaseClassControl.Complition_txt(txtParentCate, $"Select top 20 Name txt From ParentCategory Where Name Like '%{txtParentCate.Text}%'");
            BaseClassControl.Complition_txt(txtChildCate, $"Select top 20 Name txt From ChildCategory Where Name Like '%{txtChildCate.Text}%'");
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Update")
            {
                Update();
                //return;
            }
            else
            {
                pCat.Name = txtParentCate.Text;
                DataTable isExist = pCatRepository.SelectParentCategory(pCat);
                Int64 parentId = 0;
                if (isExist.Rows.Count > 0)
                    parentId = Convert.ToInt32(isExist.Rows[0].ItemArray[1]);

                if (isExist.Rows.Count == 0)
                {
                    pCat.CreatedAt = DateTime.Now;
                    pCat.CreatedBy = 1;
                    parentId = pCatRepository.InsertParentCategory(pCat);
                    if (parentId > 0)
                    {
                        cCat.Name = txtChildCate.Text;
                        cCat.CreatedAt = DateTime.Now;
                        cCat.CreatedBy = 1;
                        cCat.ParentId = Convert.ToInt32(parentId);
                        bool result = cCatRepository.InsertChildCategory(cCat);
                        if (result)
                            LoadData();
                    }
                    else
                    { }
                }
                else
                {
                    cCat.CreatedAt = DateTime.Now;
                    cCat.CreatedBy = 1;
                    cCat.Name = txtChildCate.Text;
                    cCat.ParentId = Convert.ToInt32(parentId);
                    bool result = cCatRepository.InsertChildCategory(cCat);
                    if (result)
                        LoadData();
                    txtChildCate.Text = "";
                    txtParentCate.Focus();
                }
            }
        }
        private void Update()
        {
            if (lblPCatId.Text == "")
                txtParentCate.Focus();
            else if (lblCCatId.Text == "")
                txtChildCate.Focus();
            else
            {
                pCat.Id = Convert.ToInt32(lblPCatId.Text);
                pCat.Name = txtParentCate.Text;
                pCat.UpdatedAt = DateTime.Now;
                pCat.UpdatedBy = 1;
                bool result = pCatRepository.UpdateParentCategory(pCat);
                if (result)
                {
                    cCat.Id = Convert.ToInt32(lblCCatId.Text);
                    cCat.Name = txtChildCate.Text;
                    cCat.UpdatedAt = DateTime.Now;
                    cCat.UpdatedBy = 1;
                    result = cCatRepository.UpdateChildCategory(cCat);
                    if (result)
                    {
                        btnClear.PerformClick();
                    }
                    LoadData();
                }

            }
        }
        private void LoadData()
        {
            BaseClassControl.GridViewAdd(dgvCategory, @"SELECT        ChildCategory.ParentId, ParentCategory.Name PName, ChildCategory.Id, ChildCategory.Name AS CName
FROM            ParentCategory INNER JOIN
                         ChildCategory ON ParentCategory.Id = ChildCategory.ParentId  order by  ParentCategory.Name, ChildCategory.Name");
        }
        private void dgvCategory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategory.Rows.Count > 0)
            {
                lblPCatId.Text = dgvCategory.CurrentRow.Cells[0].Value.ToString();
                txtParentCate.Text = dgvCategory.CurrentRow.Cells[1].Value.ToString();
                lblCCatId.Text = dgvCategory.CurrentRow.Cells[2].Value.ToString();
                txtChildCate.Text = dgvCategory.CurrentRow.Cells[3].Value.ToString();
                btnAdd.Text = "Update";
                btnAdd.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;

                btnDelete.Visible = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Delete this ", "Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string count = BaseClass.GetData($"Select Count(*) from ChildCategory where ParentId='{lblPCatId.Text}'");
            cCat.Id = Convert.ToInt64(lblCCatId.Text);
            bool result = cCatRepository.DeleteChildCategory(cCat); 
            if (result && Convert.ToInt16(count) == 1)
            {
                pCat.Id = Convert.ToInt64(lblPCatId.Text);
                result = pCatRepository.DeleteParentCategory(pCat);
                if (result)
                {
                    LoadData();
                    btnClear.PerformClick();
                }
            }
            else
            {
                LoadData();
                btnClear.PerformClick(); 
            }
        }
        private void txtParentCate_Leave(object sender, EventArgs e)
        {
            string pId = BaseClass.GetData($"Select Id from ParentCategory where Name='{txtParentCate.Text}'");
            if (pId != "")
                lblPCatId.Text = pId;
            txtChildCate.Focus();
        }

        private void txtChildCate_Leave(object sender, EventArgs e)
        {
            string cId = BaseClass.GetData($"Select Id from ChildCategory where Name='{txtChildCate.Text}'");
            if (cId != "")
                lblCCatId.Text = cId;
            btnAdd.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtParentCate.Text = "";
            lblPCatId.Text = "";
            dgvCategory.ClearSelection();
            txtChildCate.Text = "";
            lblCCatId.Text = "";
            btnAdd.Text = "Add";
            btnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            btnDelete.Visible = false;
            txtParentCate.Focus();
        }
    }
}
