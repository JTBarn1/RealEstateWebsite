using RealEstateLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_3
{
    public partial class ListingCreationPage : System.Web.UI.Page
    {
        ListingCreationController controller;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                controller = new ListingCreationController();
                DataSet ds = new DataSet();
                ds = controller.RetreiveAmenities();
                gvSelectAmenity.DataSource = ds;
                gvSelectAmenity.DataBind();

                Agent agent = (Agent)Session["Agent"];
                controller.SetAgent(agent);

                Session["Controller"] = controller;
            }
            else
            {
                controller = (ListingCreationController)Session["Controller"];
                UpdateRooms();
                UpdateCustomAmenities();
                UpdateImages();
            }
        }

        private void UpdateRooms()
        {
            Debug.WriteLine(tbHomeDescription.Text);
            tbodyRoomsAdd.Controls.Clear();

            int id = 0;
            foreach (DataRow row in controller.GetRoomDataTable().Rows)
            {

                TableRow roomRow = new TableRow();

                TableCell nameCell = new TableCell { Text = row["RoomName"].ToString() };
                TableCell lengthCell = new TableCell { Text = row["RoomLength"].ToString() + " ft"};
                TableCell widthCell = new TableCell { Text = row["RoomWidth"].ToString() + " ft"};
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

        private void UpdateCustomAmenities()
        {
            tbodyAmenities.Controls.Clear();

            int id = 0;
            foreach(string str in controller.GetCustomAmenities())
            {
                TableRow amenityRow = new TableRow();
                TableCell textCell = new TableCell { Text = "New Amenity:"};
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

        private void btnRemoveAmenity_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            controller.RemoveAmenity(id);
            UpdateCustomAmenities();
        }

        protected void gvSelectAmenity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            StoreCheckedAmenityIds();

            gvSelectAmenity.PageIndex = e.NewPageIndex;

            DataSet ds = controller.GetSet();

            gvSelectAmenity.DataSource = ds;

            gvSelectAmenity.DataBind();
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

                        Debug.WriteLine(path);

                        controller.AddImage(imageData, imageName,imageType, caption, path);

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

        private void UpdateImages()
        {
            Debug.WriteLine("update");
            tbodyImages.Controls.Clear();
            int id = 0;
            foreach (DataRow row in controller.GetImageDataTable().Rows)
            {
                TableRow imageRow = new TableRow();
                TableCell nameCell = new TableCell { Text = row["ImageName"].ToString() };
                TableCell typeCell = new TableCell { Text = row["ImageType"].ToString() };
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
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            controller.RemoveImage(id);
            UpdateImages();
        }

        protected void btnSubmitHouse_Click(object sender, EventArgs e)
        {
            StoreCheckedAmenityIds();

            string error = "";

            error += controller.ValidateImages();
            error += controller.ValidateAmenities();
            error += controller.ValidateRooms();

            if (error.Equals(""))
            {

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

                int numBeds = int.Parse(tbNumBedrooms.Text);
                float numBaths = float.Parse(tbNumBathrooms.Text);
                int askingPrice = int.Parse(tbaskingPrice.Text);
                int garageSize = int.Parse(ddlGarageSize.SelectedValue.ToString());



                controller.CreateListing(homeStreet, homeCity, homeState, homeZip, yearBuilt, heatType,
                coolType, waterType, sewageType, numBeds, numBaths, askingPrice, garageSize, description, homeType);

                Response.Redirect("HomePage.aspx");
            }
            else
            {
                divError.InnerHtml = "";
                divError.InnerHtml += error;
            }
        }


        //both of these are only here because THE STUPID GRIDVIEW WONT REMEBER WHAT WAS CHECKED
        //I HATE SESSION MANAGEMENT I HATE IT SO MUCH
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
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}