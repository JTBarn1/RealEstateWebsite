using RealEstateLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_3
{
    public partial class LIstingModifyPage : System.Web.UI.Page
    {
        ListingModificationController controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                controller = new ListingModificationController((Listing)Session["ListingToModify"]);

                DataRow row = controller.GetAllListingInfo();

                Street.Text = row["ListingStreet"].ToString();
                City.Text = row["ListingCity"].ToString();
                State.Text = row["Listingstate"].ToString();
                Zip.Text = row["ListingZip"].ToString();
                tbaskingPrice.Text = row["ListingPrice"].ToString();
                tbStatus.Text = row["ListingStatus"].ToString() ;
                tbNumBedrooms.Text = row["ListingNumBeds"].ToString();
                tbNumBathrooms.Text = row["ListingNumBathrooms"].ToString() ;
                tbYearBuilt.Text = row["ListingYear"].ToString();

                tbHomeDescription.Text = row["ListingDescription"].ToString();

                ddlHomeType.Text = row["ListingHomeType"].ToString();
                ddlGarageSize.Text = row["ListingGarageSize"].ToString();

                string heatType = row["ListingHeatType"].ToString();
                string coolingType = row["ListingCoolingType"].ToString();
                string waterType = row["ListingWaterType"].ToString();
                string sewerType = row["ListingSewageType"].ToString();

                string[] heatArr = { "None", "Forced Air", "Radiator" };
                string[] coolingArr = { "None", "HVAC", "Wall Mount AC" };
                string[] waterArr = { "None", "Well Water", "City Water" };
                string[] sewerArr = { "None", "City Sewage", "Septic Tank" };

                for (int i = 0; i < heatArr.Length; i++)
                {
                    if (heatType.Equals(heatArr[i]))
                        radioHeat.SelectedIndex = i;
                }

                for (int i = 0; i < coolingArr.Length; i++)
                {
                    if (coolingType.Equals(coolingArr[i]))
                        radioCooling.SelectedIndex = i;
                }

                for (int i = 0; i < waterArr.Length; i++)
                {
                    if (waterType.Equals(waterArr[i]))
                        radioWater.SelectedIndex = i;
                }

                for (int i = 0; i < sewerArr.Length; i++)
                {
                    if (sewerType.Equals(sewerArr[i]))
                        radioSewer.SelectedIndex = i;
                }

                DataSet ds = new DataSet();
                ds = controller.RetreiveAmenities();
                gvSelectAmenity.DataSource = ds;
                gvSelectAmenity.DataBind();

                UpdateImages();
                UpdateRooms();

                Session["ModifyController"] = controller;
            }
            else
            {
                controller = (ListingModificationController)Session["ModifyController"];
                UpdateRooms();
                UpdateImages();
                UpdateCustomAmenities();
            }
        }
        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            divError.InnerHtml = "";

            int imageSize;

            string imageType, imageName;


            if (fileImageAdd.HasFile)

            {
                foreach (HttpPostedFile file in fileImageAdd.PostedFiles)
                {
                    imageSize = file.ContentLength;

                    byte[] imageData = new byte[imageSize];

                    file.InputStream.Read(imageData, 0, imageSize);

                    imageName = file.FileName;
                    imageType = file.ContentType;

                    string fileExtension = imageName.Substring(imageName.LastIndexOf("."));
                    fileExtension = fileExtension.ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".bmp" || fileExtension == ".gif" || fileExtension == ".png")

                    {
                        string caption = txtImageCaption.Text;

                        string path = Server.MapPath("FileStorage/");

                        controller.AddImage(imageData, imageName, imageType, caption, path);

                        divImageTable.Visible = true;

                        UpdateImages();

                        txtImageCaption.Text = "";
                    }
                    else
                    {
                        string error = "Error: " + imageName + " Is using an incorrect file extension. </br>";
                        divError.InnerHtml += error;
                    }
                }
            }
        }
        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            controller.RemoveImage(id);
            UpdateImages();
        }
        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            int length = int.Parse(tbRoomLength.Text);
            int width = int.Parse(tbRoomWidth.Text);
            string description = tbRoomDesc.Text;

            controller.AddRoomToList(length, width, description);

            divRoomTable.Visible = true;
            UpdateRooms();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            controller.RemoveRoom(id);
            UpdateRooms();
        }

        protected void buttonAddAmenity_Click(object sender, EventArgs e)
        {
            string amenitydescription = tbAmenityNew.Text;

            controller.AddCustomAmenityToList(amenitydescription);

            divExtraAmenities.Visible = true;

            UpdateCustomAmenities();

        }

        private void btnRemoveAmenity_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            controller.RemoveAmenity(id);
            UpdateCustomAmenities();
        }

        protected void btnSaveHouse_Click(object sender, EventArgs e)
        {
            StoreCheckedAmenityIds();

            string error = "";

            error += controller.ValidateImages();
            error += controller.ValidateAmenities();
            error += controller.ValidateRooms();

            if (error.Equals("")) { 

            string homeStreet = Street.Text;
            string homeCity = City.Text;
            string homeState = State.Text.ToUpper();
            string homeZip = Zip.Text;
            string heatType = radioHeat.SelectedValue;
            string coolType = radioCooling.SelectedValue;
            string waterType = radioWater.SelectedValue;
            string sewageType = radioSewer.SelectedValue;
            string yearBuilt = tbYearBuilt.Text;
            string description = tbHomeDescription.Text;
            string homeType = ddlHomeType.Text;
            string status = tbStatus.Text;

            int numBeds = int.Parse(tbNumBedrooms.Text);
            float numBaths = float.Parse(tbNumBathrooms.Text);
            int askingPrice = int.Parse(tbaskingPrice.Text);
            int garageSize = int.Parse(ddlGarageSize.SelectedValue.ToString());

            controller.UpdateListingData(homeStreet, homeCity, homeState, homeZip, yearBuilt, heatType,
            coolType, waterType, sewageType, numBeds, numBaths, askingPrice, garageSize, description, homeType, status);


            Response.Redirect("HomePage.aspx");
            }
            else
            {
                divError.InnerHtml = "";
                divError.InnerHtml += error;
            }

        }

        private void UpdateRooms()
        {

            tbodyRoomsAdd.Controls.Clear();

            int id = 0;
            foreach (DataRow row in controller.GetRoomDataTable().Rows)
            {

                TableRow roomRow = new TableRow();

                TableCell nameCell = new TableCell { Text = row["RoomName"].ToString() };
                TableCell lengthCell = new TableCell { Text = row["RoomLength"].ToString() + " ft" };
                TableCell widthCell = new TableCell { Text = row["RoomWidth"].ToString() + " ft" };
                TableCell sizeCell = new TableCell { Text = row["RoomSize"].ToString() + " SQFT" };

                TableCell actionCell = new TableCell();

                Button btnRemove = new Button
                {
                    Text = "Remove",
                    CommandArgument = id.ToString(),
                    ID = "btnRemove_" + id
                };
                btnRemove.Click += new EventHandler(btnRemove_Click);
                actionCell.Controls.Add(btnRemove);

                roomRow.Cells.Add(nameCell);
                roomRow.Cells.Add(lengthCell);
                roomRow.Cells.Add(widthCell);
                roomRow.Cells.Add(sizeCell);
                roomRow.Cells.Add(actionCell);

                tbodyRoomsAdd.Controls.Add(roomRow);
                id++;
            }
        }

        private void UpdateCustomAmenities()
        {
            tbodyAmenities.Controls.Clear();

            int id = 0;
            foreach (string str in controller.GetCustomAmenities())
            {
                TableRow amenityRow = new TableRow();
                TableCell textCell = new TableCell { Text = "New Amenity:" };
                TableCell descriptionCell = new TableCell { Text = str };
                TableCell actionCell = new TableCell();

                Button btnRemove = new Button
                {
                    Text = "Remove",
                    CommandArgument = id.ToString(),
                    ID = "btnRemoveAmenity_" + id
                };
                btnRemove.Click += new EventHandler(btnRemoveAmenity_Click);
                actionCell.Controls.Add(btnRemove);

                amenityRow.Cells.Add(textCell);
                amenityRow.Cells.Add(descriptionCell);
                amenityRow.Cells.Add(actionCell);

                tbodyAmenities.Controls.Add(amenityRow);
                id++;
            }
        }      

        private void UpdateImages()
        {
            tbodyImages.Controls.Clear();
            int id = 0;
            foreach (DataRow row in controller.GetExistingImageDataTable().Rows)
            {
                TableRow imageRow = new TableRow();
                TableCell nameCell = new TableCell { Text = row["ImageName"].ToString() };
                TableCell typeCell = new TableCell { Text = row["ImageCaption"].ToString() };
                TableCell actionCell = new TableCell();

                Button btnRemove = new Button
                {
                    Text = "Remove",
                    CommandArgument = id.ToString(),
                    ID = "btnRemoveImage_" + id
                };
                btnRemove.Click += new EventHandler(btnRemoveImage_Click);
                actionCell.Controls.Add(btnRemove);

                imageRow.Cells.Add(nameCell);
                imageRow.Cells.Add(typeCell);
                imageRow.Cells.Add(actionCell);

                tbodyImages.Controls.Add(imageRow);
                id++;
            }
            id = 0;
            foreach(DataRow row in controller.GetNewImageDataTable().Rows)
            {
                TableRow imageRow = new TableRow();
                TableCell nameCell = new TableCell { Text = row["ImageName"].ToString() };
                TableCell typeCell = new TableCell { Text = row["ImageCaption"].ToString() };
                TableCell actionCell = new TableCell();
                Button btnRemove = new Button
                {
                    Text = "Remove",
                    CommandArgument = id.ToString(),
                    ID = "btnRemoveNewImage_" + id
                };
                btnRemove.Click += new EventHandler(btnRemoveNewImage_Click);
                actionCell.Controls.Add(btnRemove);

                imageRow.Cells.Add(nameCell);
                imageRow.Cells.Add(typeCell);
                imageRow.Cells.Add(actionCell);

                tbodyImages.Controls.Add(imageRow);
                id++;
            }
        }

        private void btnRemoveNewImage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);
            controller.RemoveNewImage(id);
            UpdateImages();
        }

        protected void gvSelectAmenity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            StoreCheckedAmenityIds();

            gvSelectAmenity.PageIndex = e.NewPageIndex;

            DataSet ds = controller.GetSet();

            gvSelectAmenity.DataSource = ds;

            gvSelectAmenity.DataBind();
        }


        private void StoreCheckedAmenityIds()
        {

            List<int> selectedIds = controller.GetCheckedamenities();
            if (selectedIds == null)
            {
                selectedIds = new List<int>();
            }

            foreach (GridViewRow row in gvSelectAmenity.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                int amenityId = int.Parse(row.Cells[0].Text);

                if (chk != null && chk.Checked)
                {
                    if (!selectedIds.Contains(amenityId))
                    {
                        selectedIds.Add(amenityId);
                    }
                }
                else
                {
                    selectedIds.Remove(amenityId);
                }
            }

            Session["Controller"] = controller;
        }

        protected void gvSelectAmenity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<int> selectedIds = controller.GetCheckedamenities();

            if (selectedIds != null)
            {
                string amenityIdString = e.Row.Cells[0].Text;
                int amenityId = 0;
                if (int.TryParse(amenityIdString, out amenityId))
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                    if (selectedIds.Contains(amenityId))
                    {
                        chk.Checked = true;
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            controller.RemoveListing();
            Response.Redirect("HomePage.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}