using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * This program is about Airline Reservation System
 * Assignment 1
 * 
 * Made by Yunce Kim(7940406), block 4
 * Level 2, Computer Programmer/Analyst
 * Conestoga College
 * 
 * Jan 27, 2019
 */
namespace YKimAssignment1
{
    public partial class Form1 : Form
    {
        //for booking system
        public string[,] arryAirReservation = new string[5, 3];
        //in order to replace the row number of seat to alphabet order
        public string[] arryRowIndex = new string[5] { "A", "B", "C", "D",
            "E" };

        public string[] arryWaitingList = new string[10];//for waiting list

        //in order to control the seat button
        public Control ctn = new Control();
        //in order to get the row and column index from the list
        int idxOfRow = 0, idxOfCol = 0;
        public string passengerName = "";
        //in order to control the number of booking and waiting
        public int cntOfSeat = 0, cntOfWL = 0; 

        public Form1()
        {
            InitializeComponent();
        }

        private void btnFillAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < arryAirReservation.GetLength(0) ; i++)
            {
                for (int j = 0; j < arryAirReservation.GetLength(1); j++)
                {
                    arryAirReservation[i, j] = "Yunice";

                    ctn = this.Controls["btn" + arryRowIndex[i] + j];
                    ctn.Text = arryAirReservation[i, j];
                    ctn.BackColor = Color.LightPink;
                    cntOfSeat++;
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtBxShowAll.Clear();
            for (int i = 0; i < arryAirReservation.GetLength(0); i++)
            {
                for (int j = 0; j < arryAirReservation.GetLength(1); j++)
                {
                    if (arryAirReservation[i, j] != null)
                    {
                        txtBxShowAll.AppendText(arryRowIndex[i] + j + " : " 
                            + arryAirReservation[i, j] + "\n");
                    }
                }
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            idxOfRow = lstBxRow.SelectedIndex;
            idxOfCol = lstBxCol.SelectedIndex;
            passengerName = txtName.Text;

            if (passengerName == "")
            {
                MessageBox.Show("Type the passenger's name.");
            }
            else if (idxOfRow == -1)
            {
                MessageBox.Show("Select the row of the seat.");
            }
            else if (idxOfCol == -1)
            {
                MessageBox.Show("Select the column of the seat.");
            }
            else
            {
                if (cntOfSeat < 15)//if the toal seat is not full
                {
                    //if the seat is empty
                    if (arryAirReservation[idxOfRow, idxOfCol] == null)
                    {
                        arryAirReservation[idxOfRow, idxOfCol] = passengerName;

                        ctn = this.Controls["btn" + arryRowIndex[idxOfRow] + 
                            idxOfCol];
                        ctn.Text = passengerName;
                        ctn.BackColor = Color.LightPink;

                        cntOfSeat++;//increase the count of the occupied seat 
                        
                        //initalize the input text of the name
                        txtName.Text = "";
                        txtStatus.Text = "";

                        MessageBox.Show("Successfully booked.");
                    }
                    else
                    {//if the seat is already taken
                        MessageBox.Show("Already taken. Choose another seat.");
                    }
                }
                else
                {
                    //In order to add the waiting list
                    btnAddToWL_Click(sender, e);
                }
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            idxOfRow = lstBxRow.SelectedIndex;
            idxOfCol = lstBxCol.SelectedIndex;

            if (idxOfCol == -1 || idxOfRow == -1)
            {
                MessageBox.Show("Select the seat first.");
            }
            else
            {
                if (arryAirReservation[idxOfRow, idxOfCol] == null)
                {
                    
                    txtStatus.Text = "Available";
                }
                else
                {
                    txtStatus.Text = "Not available";
                }
            }
        }

        private void btnAddToWL_Click(object sender, EventArgs e)
        {
            if (cntOfSeat < 15)
            {//if the total of booking is less then 16
                MessageBox.Show("Seats are available.");
            }
            else if (cntOfWL >= 10)
            {//if the total of waiting list is 10, it means full
                MessageBox.Show("Not available for waiting list.");
            }
            else
            {//if there are some space to add the passenger to the waiting list
                passengerName = txtName.Text;

                if (passengerName == "")
                {
                    MessageBox.Show("Type the passenger's name.");
                }
                else
                {                
                    for (int i = 0; i < arryWaitingList.Length; i++)
                    {
                        if (arryWaitingList[i] == null)
                        {//Add the customer to the waiting list and come out
                            arryWaitingList[i] = txtName.Text;
                            break;
                        }
                    }
                    cntOfWL++;//In order to count the number of waiting list

                    txtName.Text = "";//initalize the input text of the name
                    txtStatus.Text = "";

                    MessageBox.Show("Successfully added to waiting list.");
                }

            }
        }

        private void btnShowWL_Click(object sender, EventArgs e)
        {
            txtBxShowWL.Clear();//Clear the waiting list
            for (int i = 0; i < arryWaitingList.Length; i++)
            {
                if (arryWaitingList[i] != null)
                {
                    txtBxShowWL.AppendText("["+(i+1)+"] "+ arryWaitingList[i] 
                        + "\n");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //Initialize the memory first            
            for (int i = 0; i < arryAirReservation.GetLength(0); i++)
            {
                for (int j = 0; j < arryAirReservation.GetLength(1); j++)
                {
                    arryAirReservation[i, j] = null;
                    ctn = this.Controls["btn" + arryRowIndex[i] + j];
                    ctn.Text = "";
                    ctn.BackColor = Color.SkyBlue;
                }
            }

            for (int i = 0; i < arryWaitingList.Length; i++)
            {
                arryWaitingList[i] = null;
            }

            btnShowAll_Click(sender, e);
            btnShowWL_Click(sender, e);
            passengerName = "";
            cntOfSeat = 0;
            cntOfWL = 0;

            //Initialize the form component next
            txtName.Text = "";
            txtStatus.Text = "";
            lstBxRow.ClearSelected();
            lstBxCol.ClearSelected();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            idxOfRow = lstBxRow.SelectedIndex;
            idxOfCol = lstBxCol.SelectedIndex;

            if (idxOfRow == -1)
            {
                MessageBox.Show("Select the row to cancel.");
            }
            else if (idxOfCol == -1)
            {
                MessageBox.Show("Select the column to cancel.");
            }
            else
            {
                if (arryAirReservation[idxOfRow, idxOfCol] == null)
                {
                    MessageBox.Show("No booked seat. No need to cancel.");
                }
                else
                {
                    arryAirReservation[idxOfRow, idxOfCol] = null;

                    ctn = this.Controls["btn" + arryRowIndex[idxOfRow] 
                        + idxOfCol];
                    ctn.Text = "";
                    ctn.BackColor = Color.SkyBlue;
                    cntOfSeat--;//the total count of booking is reduced by 1

                    MessageBox.Show("Successfully cancelled.");

                    if (cntOfWL > 0)//if there are customers in the waiting list
                    {
                        //getting the first customer's name in the waiting list
                        passengerName = arryWaitingList[0];

                        //setting the first customer's name in the waiting list
                        arryAirReservation[idxOfRow, idxOfCol] = passengerName;

                        ctn = this.Controls["btn" + arryRowIndex[idxOfRow] 
                            + idxOfCol];
                        ctn.Text = passengerName;
                        ctn.BackColor = Color.LightPink;

                        cntOfSeat++;//increase the count of the occupied seat 

                        //in order to place ahead one by the order of waiting list 
                        for ( int i = 0 ; i < cntOfWL ; i++)
                        {
                            if (i < (cntOfWL - 1))
                            {//get the next turn to the front
                                arryWaitingList[i] = arryWaitingList[i + 1];
                            }
                            else
                            {//set null to the last of waiting list
                                arryWaitingList[i] = null;
                            }
                        }
                        cntOfWL--;//the total count of waiting list is reduced by 1

                        MessageBox.Show("Successfully booked for 1st customer " +
                            "in waiting list.");
                    }
                }
            }
        }
    }
}
