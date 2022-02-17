using System;
using System.Windows.Forms; 

namespace NewTemplate.Stock.Product
{
    public partial class AddCategory : Form
    {
        ChildCategoryRepo pCatRepository = new ChildCategoryRepo();
        //ChildCategory cCat;
        public AddCategory()
        {
            InitializeComponent();
        } 

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
