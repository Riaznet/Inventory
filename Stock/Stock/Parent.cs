using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Stock.Stock.Product;
using Stock.Stock;

namespace Stock
{
    public partial class Parent : Form
    {
        private Button currentButton;
        private Form activeForm;
        public Parent()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            pnlPurchase.Height = 0;
            pnlProduct.Height = 0;
            pnlSales.Height = 0;
            pnlTransfer.Height = 0;
            pnlDue.Height = 0;
            pnlCustomerSup.Height = 0;
            pnlOthers.Height = 0;
            pnlWastage.Height = 0;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximized_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSideBarLeft_Click(object sender, EventArgs e)
        {
            ToggleSideBar();
            //btnSideBarLeft.Visible = false;
            //btnSideBarRight.Visible = true;
            //pnlMouseAction.Visible = true;
        }

        private void btnSideBarRight_Click(object sender, EventArgs e)
        {
            ToggleSideBar();
            //btnSideBarLeft.Visible = true;
            //btnSideBarRight.Visible = false;
            //pnlMouseAction.Visible = false;
        }

        private void pnlSideBar_MouseHover(object sender, EventArgs e)
        {

            pnlSideBar.Width = 250;
        }

        private void pnlSideBar_MouseLeave(object sender, EventArgs e)
        {
            pnlSideBar.Width = 50;
        }

        private void Parent_MouseEnter(object sender, EventArgs e)
        {
            if (btnSideBarRight.Visible)
            {
                pnlSideBar.Width = 0;
                pnlMouseAction.Visible = true;
            }
        }
        private void pnlMouseAction_MouseEnter(object sender, EventArgs e)
        {
            if (btnSideBarRight.Visible)
            {
                pnlSideBar.Width = 250;
                pnlMouseAction.Visible = false;
            }
        }

        private void pnlMaster_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnlLogo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnlSideBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pnlFooter_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(224, 224, 224);
                    currentButton.ForeColor = Color.Black;
                    currentButton.Font = new Font("Microsoft Sans Serif", 14.5F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    btnCloseChildPanel.Visible = true;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousButton in pnlSideBar.Controls)
            {
                if (previousButton.GetType().Name == "IconButton")
                {
                    previousButton.BackColor = Color.FromArgb(11, 7, 17);
                    previousButton.ForeColor = Color.DimGray;
                    previousButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void ShowSubMenu(Panel panel)
        {
            if (panel != null)
            {
                pnlProduct.Visible = false;
                pnlSales.Visible = false;
                pnlPurchase.Visible = false;
                pnlWastage.Visible = false;
                pnlTransfer.Visible = false;
                pnlOthers.Visible = false;
                panel.Visible = true;
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();

            }
            ActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnlMaster.Controls.Add(childForm);
            this.pnlMaster.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
        }
        private void btnProduct_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new AddProduct(), sender);
            TogglePanel(pnlProduct, 350);
        }
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new Dashboard(), sender);
            TogglePanel(pnlPurchase, 309);
        }
        private void btnSales_Click(object sender, EventArgs e)
        {
            //ActiveButton(sender);
            TogglePanel(pnlSales, 263);
        }
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            TogglePanel(pnlTransfer, 85);
        }
        private void btnDue_Click(object sender, EventArgs e)
        {
            TogglePanel(pnlDue, 116);
        }
        private void btnWastage_Click(object sender, EventArgs e)
        {
            TogglePanel(pnlWastage, 86);
        }
        private void btnCustomerSup_Click(object sender, EventArgs e)
        {
            TogglePanel(pnlCustomerSup, 179);
        }
        private void btnStoreEntry_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
        }
        private void btnOthersReport_Click(object sender, EventArgs e)
        {
            TogglePanel(pnlOthers, 169);
        }

        private void btnCloseChildPanel_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "Home";
            currentButton = null;
            btnCloseChildPanel.Visible = false;
        }
        private void TogglePanel(Panel panel, int value)
        {
            int i = 0;
            if (panel.Height == 0)
            {
                while (i <= value)
                {
                    if (value < i)
                    {
                        panel.Height = value;
                        break;
                    }
                    panel.Height = i;
                    i = i + 10;
                }
                panel.Height = value;
            }
            else
            {
                while (value >= 0)
                {
                    if (value < 0)
                    {
                        panel.Height = 0;
                        break;
                    }
                    panel.Height = value;
                    value = value - 10;
                }
                panel.Height = 0;
            }
        }
        private void ToggleSideBar()
        {
            int i = 0;
            if (pnlSideBar.Width == 0)
            {
                while (i <= 250)
                {
                    if (250 < i)
                    {
                        pnlSideBar.Width = 250;
                        break;
                    }
                    i = i + 20;
                    pnlSideBar.Width = i;

                }
                pnlSideBar.Width = 250;
                pnlMouseAction.Visible = false;
            }
            else
            {
                i = 250;
                while (0 <= i)
                {
                    if (i <= 0)
                    {
                        pnlSideBar.Width = 0;
                        break;
                    }
                    i = i - 20;
                    pnlSideBar.Width = i;

                }
                pnlSideBar.Width = 0;
                pnlMouseAction.Visible = true;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddCategory(), sender);
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateBarcode_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {

        }

        private void btnProductList_Click(object sender, EventArgs e)
        {

        }

        private void btnProductStock_Click(object sender, EventArgs e)
        {

        }

        private void btnProductRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnProductUploadByExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnAddPurchase_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseReturn_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseStatement_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseReturnStatement_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseStatementStoreWise_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseStatementUserWise_Click(object sender, EventArgs e)
        {

        }

        private void btnPurchaseStatementSupplierWise_Click(object sender, EventArgs e)
        {

        }

        private void btnSaleProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnSalesReturn_Click(object sender, EventArgs e)
        {

        }

        private void btnSalesStatement_Click(object sender, EventArgs e)
        {

        }

        private void btnSalesStatementStoreWise_Click(object sender, EventArgs e)
        {

        }

        private void btnSalesStatementUserWise_Click(object sender, EventArgs e)
        {

        }

        private void btnSalesStatementPartyWise_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTransfer_Click(object sender, EventArgs e)
        {

        }

        private void btnTransferStatement_Click(object sender, EventArgs e)
        {

        }

        private void btnDueReceiveEntry_Click(object sender, EventArgs e)
        {

        }

        private void btnDueCollectionReport_Click(object sender, EventArgs e)
        {

        }

        private void btnDueInformation_Click(object sender, EventArgs e)
        {

        }

        private void btnAddWastageProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnWastageStatement_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCustomerSupplier_Click(object sender, EventArgs e)
        {

        }

        private void btnCustomerSupplierList_Click(object sender, EventArgs e)
        {

        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {

        }

        private void btnSupplierList_Click(object sender, EventArgs e)
        {

        }

        private void btnMinMaxReport_Click(object sender, EventArgs e)
        {

        }

        private void btnClosingStock_Click(object sender, EventArgs e)
        {

        }

        private void btnTopSellPurchase_Click(object sender, EventArgs e)
        {

        }

        private void btnProfitLoss_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //popupNotifier1.TitleText = txtTitle.Text;
            //popupNotifier1.ContentText = txtText.Text;
            //popupNotifier1.ShowCloseButton = chkClose.Checked;
            //popupNotifier1.ShowOptionsButton = chkMenu.Checked;
            //popupNotifier1.ShowGrip = chkGrip.Checked;
            //popupNotifier1.Delay = int.Parse(txtDelay.Text);
            //popupNotifier1.AnimationInterval = int.Parse(txtInterval.Text);
            //popupNotifier1.AnimationDuration = int.Parse(txtAnimationDuration.Text);
            //popupNotifier1.TitlePadding = new Padding(int.Parse(txtPaddingTitle.Text));
            //popupNotifier1.ContentPadding = new Padding(int.Parse(txtPaddingContent.Text));
            //popupNotifier1.ImagePadding = new Padding(int.Parse(txtPaddingIcon.Text));
            //popupNotifier1.Scroll = chkScroll.Checked;
            //popupNotifier1.IsRightToLeft = chkIsRightToLeft.Checked;
            //if (chkIcon.Checked)
            //{
            //    popupNotifier1.Image = Properties.Resources._157_GetPermission_48x48_72;
            //}
            //else
            //{
            //    popupNotifier1.Image = null;
            //}

            //popupNotifier1.Popup();
        }
    }
}
