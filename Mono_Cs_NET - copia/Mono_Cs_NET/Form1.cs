using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using JYMONOLib;
using JYCONFIGBROWSERCOMPONENTLib;
using JYSYSTEMLIBLib;

namespace MonoSimple
{
    public partial class Form1 : Form
    {
        private Boolean mbEmulate;
        private Boolean mbForceInit;
        private Boolean mbInitialized;
        private Boolean mbPositionChanged;
        private Boolean mbSlitFrontEntranceChanged;
        private Boolean mbSlitSideEntranceChanged;
        private Boolean mbSlitFrontExitChanged;
        private Boolean mbSlitSideExitChanged;
        private String sMonoDevId;
        private String sMonoName;

        public JYConfigBrowerInterface mConfigBrowser;
        private JYMONOLib.MonochromatorClass mMono;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String sDevId;
            String sDevName;
            SCDid SCD;

            try
            {
                mConfigBrowser = new JYConfigBrowerInterface();

                // Remove existing items from the control.
                comboboxMonos.Items.Clear();
                
                // Load Config Browser
                mConfigBrowser.Load();

                // File Combo Box with Mono devices
                sDevId = mConfigBrowser.GetFirstMono(out sDevName);
                while ((sDevName != null) && (String.Compare(sDevName, "") != 0))
                {
                    // Add Configuration Names and IDs to combo box
                    SCD = new SCDid();
                    SCD.sName = sDevName;
                    SCD.sID = sDevId;
                    comboboxMonos.Items.Add( SCD );

                    // Find the next Mono in this Configuration
                    sDevId = mConfigBrowser.GetNextMono(out sDevName);
                }

                if (comboboxMonos.Items.Count >= 1)
                    comboboxMonos.SelectedIndex = 0;

            }
            catch (SystemException ex)
            {
                // Received Exception: report & exit
                MessageBox.Show(String.Format("Exception: {0}", ex.Message), "Exception");
                return;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboboxMonos_SelectedIndexChanged(object sender, EventArgs e)
        {
            mbEmulate = false;
            mbForceInit = false;
            mbInitialized = false;
            labelInitStatus.Text = "Uninitialized";
            labelInitStatus.ForeColor = Color.Gray;
            buttonInitialize.Text = "Initialize";

            groupboxWlCtrl.Enabled = false;
            Frame_Slits.Enabled = false;
            Frame_Mirrors.Enabled = false;

            SCDid CurrMono = (SCDid)comboboxMonos.SelectedItem;
            sMonoDevId = CurrMono.sID;
        }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            String sDevUniqueId;
            String sDevFoundName;
            String sStatus;

            // OPEN COMMUNICATIONS
            try
            {
                // Find Selected Device UniqueId & Name
                SCDid CurrMono = (SCDid)comboboxMonos.SelectedItem;
                sMonoDevId = CurrMono.sID;

                if ((sMonoDevId == null) || (sMonoDevId.CompareTo("") == 0))
                    return;

                sDevUniqueId = mConfigBrowser.GetFirstMono(out sDevFoundName);

                while ((sDevUniqueId != null) && (sDevUniqueId.CompareTo(sMonoDevId) != 0))
                {
                    sDevUniqueId = mConfigBrowser.GetNextMono(out sDevFoundName);
                }

                sMonoName = sDevFoundName;

                // Create New MonochromatorClass
                mMono = new JYMONOLib.MonochromatorClass();

                mMono.Uniqueid = sDevUniqueId;

                // Set Mono Initialize event handler
                mMono._IJYDeviceReqdEvents_Event_Initialize += OnReceivedInitMono;

                // Loads up the device with the specified configuration
                mMono.Load();

                tbStatus.Clear();
                sStatus = String.Format("Opening Communications ... ");
                tbStatus.AppendText(sStatus);

                // Attempts to communicate with a device on the specified communication 
                // settings (in the configuration). If it fails, the catch allows the
                // device to be emulated in software.
                mMono.OpenCommunications();

                // OpenCommunications() succeeded - no need to Emulate
                mbEmulate = false;
                
                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);

            }
            catch (Exception ex)
            {
                sStatus = String.Format("Failed{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);

                DialogResult dr; 
                sStatus = String.Format(
                    "{0} Hardware Not Detected{1}{1}Do you want to emulate this device?",
                    sMonoName, Environment.NewLine);
                dr = MessageBox.Show(
                    sStatus, "Mono Hardware Not Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(dr == DialogResult.Yes)
                {
                    // Yes: Emulate
                    mbEmulate = true;
                    sStatus = String.Format("Emulating {0} ...{1}", sMonoName, Environment.NewLine);
                    tbStatus.AppendText(sStatus);
                }
                else if (dr == DialogResult.No)
                {
                    // No: Do not Emulate
                    mbEmulate = false;
                    sStatus = String.Format("Check {0} Hardware and attempt again.", sMonoName);
                    tbStatus.AppendText(sStatus);
                    return;
                }
            }


            // INITIALIZE
            try
            {
                // Disable Intialize button while Initializing - could take time
                buttonInitialize.Enabled = false;
                labelInitStatus.Text = "Initializing...";
                labelInitStatus.ForeColor = Color.Black;

                sStatus = String.Format("Initializing Mono ... ");
                tbStatus.AppendText(sStatus);

                if (mbInitialized == true)
                    mbForceInit = true;

                mMono.Initialize(mbForceInit, mbEmulate, false);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("{0} Initialize Failed.{1}", sMonoName, Environment.NewLine);
                MessageBox.Show(sStatus, "Mono Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Disable Intialize button while Initializing - could take time
                buttonInitialize.Enabled = true;
                buttonInitialize.Text = "Initialize";
                labelInitStatus.Text = "Error";
                labelInitStatus.ForeColor = Color.Red;
            }
        }

        // Declaration of an event handler for HJY Mono
        public void OnReceivedInitMono(int status, JYSYSTEMLIBLib.IJYEventInfo myEvent)
        {
             String sStatus;
             jyUnits Units;
             Object oUnitsString;

             buttonInitialize.Enabled = true;

            if (status == 0)
            {
                mbInitialized = true;
                buttonInitialize.Enabled = true;
                buttonInitialize.Text = "Reinitialize";
                if (mbEmulate == false)
                {
                    labelInitStatus.ForeColor = Color.Green;
                    labelInitStatus.Text = "Initialized";
                }
                else
                {
                    labelInitStatus.ForeColor = Color.Orange;
                    labelInitStatus.Text = "Emulating";
                }

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);

                GetPosition();
                groupboxWlCtrl.Enabled = true;     // Always enabled if successful init

                GetSlits();
                GetGratings();
                GetMirrors();

                Application.DoEvents();

                sStatus = String.Format("Current Position {0}{1}", textboxPosition.Text, Environment.NewLine);
                tbStatus.AppendText(sStatus);
                sStatus = String.Format("Current Grating {0}{1}", comboboxGrating.SelectedItem.ToString(), Environment.NewLine);
                tbStatus.AppendText(sStatus);

                mMono.GetDefaultUnits( jyUnitsType.jyutSlitWidth, out Units, out oUnitsString );
                Label_Slit_Entrance_Units.Text = oUnitsString.ToString();
                Label_Slit_Exit_Units.Text = oUnitsString.ToString();
            }
            else
            {
                mbInitialized = false;
                buttonInitialize.Enabled = true;
                buttonInitialize.Text = "Initialize";
                labelInitStatus.Text = "Failed";
                labelInitStatus.ForeColor = Color.Red;

                sStatus = String.Format("Failed{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
        }

        private void GetPosition()
        {
            Double dCurrPosition = 0;
            dCurrPosition = mMono.GetCurrentWavelength();
            textboxPosition.Text = dCurrPosition.ToString("0.00");
            mbPositionChanged = false;
        }

        private void textboxPosition_TextChanged(object sender, EventArgs e)
        {
            mbPositionChanged = true;
        }

        private void textboxPosition_KeyDown(object sender, KeyEventArgs e)
        {
            System.EventArgs ea = null;
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                textboxPosition_Leave(sender, ea);
            }
        }

        private void textboxPosition_Leave(object sender, EventArgs e)
        {
            Boolean bBusy = true;
            String sStatus;
            Double dCurrPosition = 0;

            if (mbPositionChanged == false)
                return;
            else
                mbPositionChanged = false;

            try
            {
                dCurrPosition = Convert.ToDouble(textboxPosition.Text);

                sStatus = String.Format("Moving Mono to Position {0}... ", textboxPosition.Text);
                tbStatus.AppendText(sStatus);
                mMono.MovetoWavelength(dCurrPosition);

                while (bBusy == true)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("Invalid Position Value = {0}", textboxPosition.Text);
                MessageBox.Show(sStatus, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            GetPosition();
        }
        
        private void GetGratings()
        {
            int nCurrTurr;
            Double dCurrTurr;
            Object oGratings;
            Array arrGratings;

            comboboxGrating.Items.Clear();

            nCurrTurr = mMono.GetCurrentTurret();

            mMono.GetCurrentGrating(out dCurrTurr, out oGratings);

            try
            {
                comboboxGrating.BeginUpdate();
                arrGratings = (Array)oGratings;
                foreach (double grating in arrGratings)
                {
                    comboboxGrating.Items.Add(grating);
                }
                comboboxGrating.EndUpdate();
                comboboxGrating.Update();

                //comboboxGrating.SelectedIndexChanged -= comboboxGrating_SelectedIndexChanged;
                comboboxGrating.SelectedIndexChanged -= new EventHandler(comboboxGrating_SelectedIndexChanged);

                comboboxGrating.SelectedIndex = nCurrTurr;
                comboboxGrating.SelectedIndexChanged += new EventHandler(comboboxGrating_SelectedIndexChanged);
                //comboboxGrating.SelectedIndexChanged += comboboxGrating_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboboxGrating_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean bBusy;
            String sStatus;
            int nSelectedTurr;

            try
            {
                sStatus = String.Format("Moving Mono to Grating {0}... ", comboboxGrating.SelectedItem.ToString());
                tbStatus.AppendText(sStatus);

                nSelectedTurr = comboboxGrating.SelectedIndex;
                mMono.MovetoTurret(nSelectedTurr);

                bBusy = true;
                while (bBusy == true)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);

                GetPosition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///////////////////////////////////// Slits //////////////////////////////////////

        private void GetSlits()
        {
            //get slit width from the component
            // Check to see if slit of each type is installed
            bool IsInstalled = true, bEitherEnt = false, bEitherExit = false;
            double slitWidthFE = 0, slitWidthFX = 0, slitWidthSE = 0, slitWidthSX = 0;

            //Front_Entrance slit
            IsInstalled = mMono.IsSubItemInstalled(MonoSubItemType.Slit_Front_Entrance);
            Text_Slit_Front_Entrance.Enabled = IsInstalled;
            if (IsInstalled)
            {
                slitWidthFE = mMono.GetCurrentSlitWidth(SlitLocation.Front_Entrance);
                Text_Slit_Front_Entrance.Text = Convert.ToString(slitWidthFE);
                bEitherEnt = true;
            }
            else
            {
                Text_Slit_Front_Entrance.Text = "";
            }

            //Front_Exit slit
            IsInstalled = mMono.IsSubItemInstalled(MonoSubItemType.Slit_Front_Exit);
            Text_Slit_Front_Exit.Enabled = IsInstalled;
            if (IsInstalled)
            {
                slitWidthFX = mMono.GetCurrentSlitWidth(SlitLocation.Front_Exit);
                Text_Slit_Front_Exit.Text = Convert.ToString(slitWidthFX);
                bEitherEnt = true;
            }
            else
            {
                Text_Slit_Front_Exit.Text = "";
            }

            //Side_Entrance slit
            IsInstalled = mMono.IsSubItemInstalled(MonoSubItemType.Slit_Side_Entrance);
            Text_Slit_Side_Entrance.Enabled = IsInstalled;
            if (IsInstalled)
            {
                slitWidthSE = mMono.GetCurrentSlitWidth(SlitLocation.Side_Entrance);
                Text_Slit_Side_Entrance.Text = Convert.ToString(slitWidthSE);
                bEitherExit = true;
            }
            else
            {
                Text_Slit_Side_Entrance.Text = "";
            }

            //Side_Exit slit
            IsInstalled = mMono.IsSubItemInstalled(MonoSubItemType.Slit_Side_Exit);
            Text_Slit_Side_Exit.Enabled = IsInstalled;
            if (IsInstalled)
            {
                slitWidthSX = mMono.GetCurrentSlitWidth(SlitLocation.Side_Exit);
                Text_Slit_Side_Exit.Text = Convert.ToString(slitWidthSX);
                bEitherExit = true;
            }
            else
            {
                Text_Slit_Side_Exit.Text = "";
            }

            if( ( bEitherEnt == true ) || ( bEitherEnt == true ) )
                Frame_Slits.Enabled = true;
            else
                Frame_Slits.Enabled = false;
        }

        ///////////////////////////////////// Slit Front Entrance //////////////////////////////////////

        private void Text_Slit_Front_Entrance_TextChanged(object sender, EventArgs e)
        {
            mbSlitFrontEntranceChanged = true;
        }

        private void Text_Slit_Front_Entrance_KeyDown(object sender, KeyEventArgs e)
        {
            System.EventArgs ea = null;
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                Text_Slit_Front_Entrance_Leave(sender, ea);
            }
        }

        private void Text_Slit_Front_Entrance_Leave(object sender, EventArgs e)
        {
            Boolean bBusy = true;
            String sStatus;
            Double dWidth = 0;

            if (mbSlitFrontEntranceChanged == false)
                return;
            else
                mbSlitFrontEntranceChanged = false;

            try
            {
                dWidth = Convert.ToDouble(Text_Slit_Front_Entrance.Text);

                sStatus = String.Format("Moving Front Entrance Slit to Position {0}... ", Text_Slit_Front_Entrance.Text);
                tbStatus.AppendText(sStatus);

                mMono.MovetoSlitWidth(SlitLocation.Front_Entrance, dWidth);

                while (bBusy)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }
                dWidth = mMono.GetCurrentSlitWidth(SlitLocation.Front_Entrance);

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("Invalid Front Entrance Slit Position Value = {0}", Text_Slit_Front_Entrance.Text);
                MessageBox.Show(sStatus, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Text_Slit_Front_Entrance.Text = dWidth.ToString("0.00");
        }

        ///////////////////////////////////// Slit Side Entrance //////////////////////////////////////

        private void Text_Slit_Side_Entrance_TextChanged(object sender, EventArgs e)
        {
            mbSlitSideEntranceChanged = true;
        }

        private void Text_Slit_Side_Entrance_KeyDown(object sender, KeyEventArgs e)
        {
            System.EventArgs ea = null;
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                Text_Slit_Side_Entrance_Leave(sender, ea);
            }
        }

        private void Text_Slit_Side_Entrance_Leave(object sender, EventArgs e)
        {
            Boolean bBusy = true;
            String sStatus;
            Double dWidth = 0;

            if (mbSlitSideEntranceChanged == false)
                return;
            else
                mbSlitSideEntranceChanged = false;

            try
            {
                dWidth = Convert.ToDouble(Text_Slit_Side_Entrance.Text);

                sStatus = String.Format("Moving Side Entrance Slit to Position {0}... ", Text_Slit_Side_Entrance.Text);
                tbStatus.AppendText(sStatus);

                mMono.MovetoSlitWidth(SlitLocation.Side_Entrance, dWidth);

                while (bBusy)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }
                dWidth = mMono.GetCurrentSlitWidth(SlitLocation.Side_Entrance);

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("Invalid Side Entrance Slit Position Value = {0}", Text_Slit_Side_Entrance.Text);
                MessageBox.Show(sStatus, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Text_Slit_Side_Entrance.Text = dWidth.ToString("0.00");
        }

        ///////////////////////////////////// Slit Front Exit //////////////////////////////////////

        private void Text_Slit_Front_Exit_TextChanged(object sender, EventArgs e)
        {
            mbSlitFrontExitChanged = true;
        }

        private void Text_Slit_Front_Exit_KeyDown(object sender, KeyEventArgs e)
        {
            System.EventArgs ea = null;
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                Text_Slit_Front_Exit_Leave(sender, ea);
            }
        }

        private void Text_Slit_Front_Exit_Leave(object sender, EventArgs e)
        {
            Boolean bBusy = true;
            String sStatus;
            Double dWidth = 0;

            if (mbSlitFrontExitChanged == false)
                return;
            else
                mbSlitFrontExitChanged = false;

            try
            {
                dWidth = Convert.ToDouble(Text_Slit_Front_Exit.Text);

                sStatus = String.Format("Moving Front Exit Slit to Position {0}... ", Text_Slit_Front_Exit.Text);
                tbStatus.AppendText(sStatus);

                mMono.MovetoSlitWidth(SlitLocation.Front_Exit, dWidth);

                while (bBusy)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }
                dWidth = mMono.GetCurrentSlitWidth(SlitLocation.Front_Exit);

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("Invalid Front Exit Slit Position Value = {0}", Text_Slit_Front_Exit.Text);
                MessageBox.Show(sStatus, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Text_Slit_Front_Exit.Text = dWidth.ToString("0.00");
        }

        ///////////////////////////////////// Slit Side Exit //////////////////////////////////////

        private void Text_Slit_Side_Exit_TextChanged(object sender, EventArgs e)
        {
            mbSlitSideExitChanged = true;
        }

        private void Text_Slit_Side_Exit_KeyDown(object sender, KeyEventArgs e)
        {
            System.EventArgs ea = null;
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                Text_Slit_Side_Exit_Leave(sender, ea);
            }
        }

        private void Text_Slit_Side_Exit_Leave(object sender, EventArgs e)
        {
            Boolean bBusy = true;
            String sStatus;
            Double dWidth = 0;

            if (mbSlitSideExitChanged == false)
                return;
            else
                mbSlitSideExitChanged = false;

            try
            {
                dWidth = Convert.ToDouble(Text_Slit_Side_Exit.Text);

                sStatus = String.Format("Moving Side Exit Slit to Position {0}... ", Text_Slit_Side_Exit.Text);
                tbStatus.AppendText(sStatus);

                mMono.MovetoSlitWidth(SlitLocation.Side_Exit, dWidth);

                while (bBusy)
                {
                    Application.DoEvents();
                    bBusy = mMono.IsBusy();
                }
                dWidth = mMono.GetCurrentSlitWidth(SlitLocation.Side_Exit);

                sStatus = String.Format("Complete{0}", Environment.NewLine);
                tbStatus.AppendText(sStatus);
            }
            catch (Exception ex)
            {
                sStatus = String.Format("Invalid Side Exit Slit Position Value = {0}", Text_Slit_Side_Exit.Text);
                MessageBox.Show(sStatus, "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Text_Slit_Side_Exit.Text = dWidth.ToString("0.00");
        }

        ///////////////////////////////////// Mirrors //////////////////////////////////////

        void GetMirrors()
        {
	        MirrorLocation locationEN, locationEX;
	        Boolean bIsInstalled = true, bEnt = false, bExit = false;

	        //Entrance mirror
	        bIsInstalled = mMono.IsSubItemInstalled( MonoSubItemType.Mirror_Entrance );
	        if( bIsInstalled )
	        {
                Frame_Mirror_Entrance.Enabled = true;
                bEnt = true;

                locationEN = mMono.GetCurrentMirrorPosition(MirrorLocation.EntranceMirror);
		        switch( locationEN )
		        {
		            case MirrorLocation.Front:
			            Radio_Entrance_Axial.Checked = true;
			            break;

		            case MirrorLocation.Side:
			            Radio_Entrance_Lateral.Checked = true;
			            break;

		            default:
			            break;
		        }
	        }
	        else // if its not installed, disable it
            {
                Frame_Mirror_Entrance.Enabled = false;
            }

	        //Exit mirror
	        bIsInstalled = mMono.IsSubItemInstalled( MonoSubItemType.Mirror_Exit );
	        if( bIsInstalled )
	        {
                Frame_Mirror_Exit.Enabled = true;
                bExit = true;
                
                locationEX = mMono.GetCurrentMirrorPosition(MirrorLocation.ExitMirror);
		        switch( locationEX )
		        {
    		        case MirrorLocation.Front:
	    		        Radio_Exit_Axial.Checked = true;
		    	        break;

		            case MirrorLocation.Side:
			            Radio_Exit_Lateral.Checked = true;
			            break;

		            default:
			            break;
		        }
	        }
	        else // if its not installed, disable it
	        {
                Frame_Mirror_Exit.Enabled = true;
	        }

            if (( bEnt == true) || ( bExit == true))
                Frame_Mirrors.Enabled = true;
            else
                Frame_Mirrors.Enabled = false;
        }

        ///////////////////////////////////// Entrance Mirror //////////////////////////////////////

        private void Radio_Entrance_Axial_Click(object sender, EventArgs e)
        {
            String sStatus;
            MirrorLocation locationEN;
            locationEN = mMono.GetCurrentMirrorPosition( MirrorLocation.EntranceMirror );

            if (locationEN != MirrorLocation.Front)
            {
                try
                {
                    sStatus = String.Format( "Moving Entrance Mirror to Axial (Front) Position..." );
                    tbStatus.AppendText( sStatus );

                    mMono.MovetoMirrorPosition( MirrorLocation.EntranceMirror, MirrorLocation.Front );

                    Boolean isMonoBusy = true;
                    while (isMonoBusy)
                    {
                        Application.DoEvents();
                        isMonoBusy = mMono.IsBusy();
                    }

                    sStatus = String.Format("Complete{0}", Environment.NewLine);
                    tbStatus.AppendText(sStatus);
                }
                catch (Exception ex)
                {
                    sStatus = String.Format( "Error Moving Entrance Mirror to Axial (Front) Position" );
                    MessageBox.Show( sStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                }
            }
	    }

        private void Radio_Entrance_Lateral_Click(object sender, EventArgs e)
        {
            String sStatus;
            MirrorLocation locationEN;
            locationEN = mMono.GetCurrentMirrorPosition( MirrorLocation.EntranceMirror );

            if( locationEN != MirrorLocation.Side )
            {
                try
                {
                    sStatus = String.Format( "Moving Entrance Mirror to Lateral (Side) Position..." );
                    tbStatus.AppendText(sStatus);

                    mMono.MovetoMirrorPosition( MirrorLocation.EntranceMirror, MirrorLocation.Side );

                    Boolean isMonoBusy = true;
                    while( isMonoBusy )
                    {
                        Application.DoEvents();
                        isMonoBusy = mMono.IsBusy();
                    }

                    sStatus = String.Format("Complete{0}", Environment.NewLine);
                    tbStatus.AppendText(sStatus);
                }
                catch (Exception ex)
                {
                    sStatus = String.Format( "Error Moving Entrance Mirror to Lateral (Side) Position" );
                    MessageBox.Show(sStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Radio_Exit_Axial_Click(object sender, EventArgs e)
        {
            String sStatus;
            MirrorLocation locationEX;
            locationEX = mMono.GetCurrentMirrorPosition( MirrorLocation.ExitMirror );

            if( locationEX != MirrorLocation.Front )
            {
                try
                {
                    sStatus = String.Format( "Moving Exit Mirror to Axial (Front) Position..." );
                    tbStatus.AppendText(sStatus);

                    mMono.MovetoMirrorPosition( MirrorLocation.ExitMirror, MirrorLocation.Front );

                    Boolean isMonoBusy = true;
                    while (isMonoBusy)
                    {
                        Application.DoEvents();
                        isMonoBusy = mMono.IsBusy();
                    }

                    sStatus = String.Format("Complete{0}", Environment.NewLine);
                    tbStatus.AppendText(sStatus);
                }
                catch (Exception ex)
                {
                    sStatus = String.Format("Error Moving Exit Mirror to Axial (Front) Position");
                    MessageBox.Show(sStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Radio_Exit_Lateral_Click(object sender, EventArgs e)
        {
            String sStatus;
            MirrorLocation locationEX;
            locationEX = mMono.GetCurrentMirrorPosition(MirrorLocation.ExitMirror);

            if (locationEX != MirrorLocation.Side)
            {
                try
                {
                    sStatus = String.Format("Moving Exit Mirror to Lateral (Side) Position...");
                    tbStatus.AppendText(sStatus);

                    mMono.MovetoMirrorPosition(MirrorLocation.ExitMirror, MirrorLocation.Side);

                    Boolean isMonoBusy = true;
                    while (isMonoBusy)
                    {
                        Application.DoEvents();
                        isMonoBusy = mMono.IsBusy();
                    }

                    sStatus = String.Format("Complete{0}", Environment.NewLine);
                    tbStatus.AppendText(sStatus);
                }
                catch (Exception ex)
                {
                    sStatus = String.Format("Error Moving Exit Mirror to Lateral (Side) Position");
                    MessageBox.Show(sStatus, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

    } // public partial class Form1 : Form

    public class SCDid
    {
        public string sName;
        public string sID;
        public override string ToString()
        {
            return sName.ToString();
        }
    }

} // namespace MonoSimple
