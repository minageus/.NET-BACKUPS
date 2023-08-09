using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.SQLite;
using System.Globalization;
using System.Data.SqlClient;


#pragma warning disable CS0649
#pragma warning disable IDE0044
#pragma warning disable IDE0051
#pragma warning disable IDE1006
#pragma warning disable IDE0052
#pragma warning disable CS0169

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string ReadResult = string.Empty;
        string SerialPortName = string.Empty;
        // Ότι δεδομένα δέχεται η serialPort_DataReceived, στέλνονται με BeginInvoke στην
        // SerialDataReceivedHandler, η οποία τα κάνει append στην serialReceiveBuffer
        // και κατόπιν τα εξυπηρετεί από εκεί.
        private readonly List<byte> serialReceiveBuffer = new List<byte>();
        private delegate void SerialDataReceived(byte[] data);

        private readonly List<byte> serialSendBuffer = new List<byte>();
        private delegate void SerialDataSent(byte[] data);
        static int commtickcounter;
        CommState commState = CommState.none;


        static string databaseFilename = "C:\\Users\\dminagias\\Desktop\\.NET BACKUPS\\WindowsFormsApp2\\bin\\HeaterLogs.db";
        static string connectionString = String.Format("Data Source = \"{0}\"; Version = 3;", databaseFilename);
        SQLiteConnection dbConnection1 = null;



        SQLiteConnectionStringBuilder conn = new SQLiteConnectionStringBuilder("Data Source=MyDatabase.sqlite;Version=3;");
        //conn.Open();
        //conn.ChangePassword("mypassword");


        enum CommState
        {
            none,
            sentrc,
            sentwc
        }

        private CommState state;


        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
 

            string selectCmd = "SELECT Timestamp, EventType, Temperature FROM Logs;";
            try
            {
                dbConnection1 = new SQLiteConnection(connectionString);
                dbConnection1.Open();

                using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                {
                    dbConnection.Open();
                    using (SQLiteCommand dbCommand = new SQLiteCommand(selectCmd, dbConnection))
                    using (SQLiteDataReader reader = dbCommand.ExecuteReader())
                    {
                        // Θα έχουμε διαβάσει με το SeqNumber σε αντίστροφη σειρά, οπότε αν υπάρχει κάποια εγγραφή
                        // θα είναι η τελευταία σήμανση του τρέχοντος Ζ.
                        while (reader.Read())
                        {
                            string reader_timestamp = reader.GetString(0);
                            int reader_EventType = reader.GetInt32(1);
                            string reader_Temperature = reader.GetString(2);
                            // Κάνουμε ότι θέλουμε με τα δεδομένα που διαβάσαμε…
                        }
                        reader.Close();
                    }
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                // Σφάλμα στην ανάγνωση της βάσης.
            }




            this.comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (this.comboBox1.Items.Count == 0)
            {
                this.comboBox1.Items.Add("COM1");
            }

            timer1.Start();
        }

        private void buttonReadConf_Click(object sender, EventArgs e)
        {
            if (!this.serialPort1.IsOpen)
            {
                MessageBox.Show("Serial Port is closed");
                return;
            }
            string stringToSend = "rc\r";
            byte[] bytesToSend = Encoding.ASCII.GetBytes(stringToSend);
            this.serialPort1.Write(bytesToSend, 0, bytesToSend.Length);
            commtickcounter = 0;
            commState = CommState.sentrc;
        }

        private void SerialDataReceivedHandler(byte[] dataReceived)
        {
            this.serialReceiveBuffer.AddRange(dataReceived);
            int indexOfLf = this.serialReceiveBuffer.FindIndex(x => x == 0x0a);

 


            if (indexOfLf >= 0)
            {
                var message = this.serialReceiveBuffer.GetRange(0, indexOfLf + 1);
                string messageAsString = Encoding.ASCII.GetString(message.ToArray());
                this.serialReceiveBuffer.RemoveRange(0, indexOfLf + 1);

                Debug.Write(Encoding.ASCII.GetString(message.ToArray()));

                if (messageAsString.StartsWith("fb "))
                {
                    messageAsString.TrimEnd(new char[] { '\r', '\n' });
                    var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length == 2)
                    {
                        try
                        {
                            string insertCmd = "INSERT INTO Logs (Timestamp, EventType, Temperature) VALUES ($Timestamp, $EventType, $Temperature);";
                            using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                            {
                                dbConnection.Open();
                                using (SQLiteCommand dbCommand = new SQLiteCommand(insertCmd, dbConnection))
                                {
                                    dbCommand.Parameters.Add("$Timestamp", DbType.AnsiString).Value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                                    dbCommand.Parameters.Add("$EventType", DbType.Int32).Value = 1;
                                    dbCommand.Parameters.Add("$Temperature", DbType.AnsiString).Value = fields[1];
                                    dbCommand.ExecuteNonQuery();
                                }
                                dbConnection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Σφάλμα στην εγγραφή της βάσης.
                        }
                    }
                }
                
                if (messageAsString.StartsWith("fe "))
                {
                    messageAsString = messageAsString.TrimEnd(new char[] { '\r', '\n' });
                    var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length == 2)
                    {
                        try
                        {
                            string insertCmd = "INSERT INTO Logs (Timestamp, EventType, Temperature) VALUES ($Timestamp, $EventType, $Temperature);";
                            using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                            {
                                dbConnection.Open();
                                using (SQLiteCommand dbCommand = new SQLiteCommand(insertCmd, dbConnection))
                                {
                                    dbCommand.Parameters.Add("$Timestamp", DbType.AnsiString).Value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                                    dbCommand.Parameters.Add("$EventType", DbType.Int32).Value = 2;
                                    dbCommand.Parameters.Add("$Temperature", DbType.AnsiString).Value = fields[1];
                                    dbCommand.ExecuteNonQuery();
                                }
                                dbConnection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Σφάλμα στην εγγραφή της βάσης.
                        }
                    }
                }


                if (messageAsString.StartsWith("tb"))
                {
                    messageAsString = messageAsString.TrimEnd(new char[] { '\r', '\n' });
                    var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length == 2)
                    {
                        try
                        {
                            string insertCmd = "INSERT INTO Logs (Timestamp, EventType, Temperature) VALUES ($Timestamp, $EventType, $Temperature);";
                            using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                            {
                                dbConnection.Open();
                                using (SQLiteCommand dbCommand = new SQLiteCommand(insertCmd, dbConnection))
                                {
                                    dbCommand.Parameters.Add("$Timestamp", DbType.AnsiString).Value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                                    dbCommand.Parameters.Add("$EventType", DbType.Int32).Value = 3;
                                    dbCommand.ExecuteNonQuery();
                                }
                                dbConnection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Σφάλμα στην εγγραφή της βάσης.
                        }
                    }
                }


                if (messageAsString.StartsWith("te"))
                {
                    messageAsString = messageAsString.TrimEnd(new char[] { '\r', '\n' });
                    var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length == 2)
                    {
                        try
                        {
                            string insertCmd = "INSERT INTO Logs (Timestamp, EventType, Temperature) VALUES ($Timestamp, $EventType, $Temperature);";
                            using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                            {
                                dbConnection.Open();
                                using (SQLiteCommand dbCommand = new SQLiteCommand(insertCmd, dbConnection))
                                {
                                    dbCommand.Parameters.Add("$Timestamp", DbType.AnsiString).Value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                                    dbCommand.Parameters.Add("$EventType", DbType.Int32).Value = 3;
                                    dbCommand.ExecuteNonQuery();
                                }
                                dbConnection.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Σφάλμα στην εγγραφή της βάσης.
                        }
                    }
                }

                var startTimeSpan = TimeSpan.Zero;
                var periodTimeSpan = TimeSpan.FromMinutes(10);

                var timer = new System.Threading.Timer((e) =>
                {
                    messageAsString = messageAsString.TrimEnd(new char[] { '\r', '\n' });
                    var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        string insertCmd = "INSERT INTO Logs (Timestamp, EventType, Temperature) VALUES ($Timestamp, $EventType, $Temperature);";
                        using (SQLiteConnection dbConnection = new SQLiteConnection(connectionString))
                        {
                            dbConnection.Open();
                            using (SQLiteCommand dbCommand = new SQLiteCommand(insertCmd, dbConnection))
                            {
                                dbCommand.Parameters.Add("$Timestamp", DbType.AnsiString).Value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                                dbCommand.Parameters.Add("$EventType", DbType.Int32).Value = 3;
                                dbCommand.Parameters.Add("$Temperature", DbType.AnsiString).Value = fields[1];
                                dbCommand.ExecuteNonQuery();
                            }
                            dbConnection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Σφάλμα στην εγγραφή της βάσης.
                    }
                }, null, startTimeSpan, periodTimeSpan);

                if (commState == CommState.sentrc)
                {
                    if (messageAsString.StartsWith("rc "))
                    {
                        messageAsString.TrimEnd(new char[] { '\r', '\n' });
                        var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (fields.Length == 6)
                        {
                            int value1 = -1, value2 = -1, value3 = -1, value4 = -1, value5 = -1;
                            try
                            {
                                value1 = Convert.ToInt32(fields[1]);
                                value2 = Convert.ToInt32(fields[2]);
                                value3 = Convert.ToInt32(fields[3]);
                                value4 = Convert.ToInt32(fields[4]);
                                value5 = Convert.ToInt32(fields[5]);
                            }
                            catch (Exception)
                            {
                            }
                            if (value1 >= 3 && value1 <= 40 && value2 >= 3 && value2 <= 40 &&
                                value1 + 3 < value2 && value3 >= 15 && value3 <= 100 && value4 >= 15
                                && value4 <= 100 && value3 + 40 < value4 && value5 >= 1000 && value5 < 15000)
                            {
                                nud_startHeater.Value = value1;
                                nud_stopHeater.Value = value2;
                                nud_fan_idle.Value = value3;
                                nud_fan_working.Value = value4;
                                nud_delay.Value = value5;

                                commState = CommState.none;
                                MessageBox.Show("Succesfully read RC");
                            }
                            else
                            {
                                if(value1 < 3)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Start heater temperature should be at least 3oC");
                                }
                                else if(value1 > 40)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Start heater temperature should be at most 40oC");
                                }
                                else if (value2 < 3)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Stop heater temperature should be at least 3oC");
                                }
                                else if (value2 > 40)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Stop heater temperature should be at most 40oC");
                                }
                                else if (value3 < 15)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Idle fan PWM percentage should be at least 3oC");

                                }
                                else if (value3 > 100)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Idle fan PWM percentage should be at most 40oC");
                                }
                                else if (value3 < 15)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Working fan PWM percentage should be at least 3oC");

                                }
                                else if (value3 > 100)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Working fan PWM percentage should be at most 40oC");
                                }
                                else if(value1 + 3 > value2)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Stopping heater temperature must be at least 3 degrees more that starting heater temperature");
                                }
                                else if(value3 + 40 > value4)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Working fan PWM percentage must be at least 40% more than the idle fan PWM percentage");
                                }
                                else if(value5 <1000)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Delay should be at least 1000 msec");
                                }
                                else if(value5 > 15000)
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("Delay should be at most 15000msec");
                                }
                                else
                                {
                                    commState = CommState.none;
                                    MessageBox.Show("RC message had errors");
                                }
                            }
                        }
                        else
                        {
                            commState = CommState.none;
                            MessageBox.Show("Received wrong RC response.");
                        }
                    }
                }
                else if (commState == CommState.sentwc)
                {
                    if (messageAsString.StartsWith("wc "))
                    {
                        messageAsString.TrimEnd(new char[] { '\r', '\n' });
                        var fields = messageAsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (fields.Length == 6)
                        {
                            int value1 = -1, value2 = -1, value3 = -1, value4 = -1, value5 = -1;
                            try
                            {
                                value1 = Convert.ToInt32(fields[1]);
                                value2 = Convert.ToInt32(fields[2]);
                                value3 = Convert.ToInt32(fields[3]);
                                value4 = Convert.ToInt32(fields[4]);
                                value5 = Convert.ToInt32(fields[5]);
                            }
                            catch (Exception)
                            {
                            }
                            if (value1 == nud_startHeater.Value && value2 == nud_stopHeater.Value && value3 == nud_fan_idle.Value && value4 == nud_fan_working.Value
                                && value5 == nud_delay.Value)
                            {
                                    commState = CommState.none;
                                    MessageBox.Show("Succesfully verified WC");                
                            }
                            else
                            {
                                commState = CommState.none;
                                MessageBox.Show("WC message verification error");
                            }
                        }
                        else
                        {
                            commState = CommState.none;
                            MessageBox.Show("Received wrong RC response.");
                        }
                    }
                    else if (messageAsString.StartsWith("err"))
                    {
                        MessageBox.Show("Device responce is 'err'.");

                    }

                }
            }
            if (this.serialReceiveBuffer.Count > 1000)
            {
                this.serialReceiveBuffer.Clear();
            }
        }



        private void bwRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ReadResult.Length > 0)
            {
                MessageBox.Show("ERROR {0}\n", ReadResult);
            }
            else
            {
                MessageBox.Show("SUCCESS\n");
            }
        }

        private void buttonPortOpen_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPort1.PortName = this.comboBox1.Text.Trim();
                this.serialPort1.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Error opeining serial port");
                return;
            }

        }

        private void buttonPortClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.serialPort1.Close();
            }
            catch (Exception)
            {
            }

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == System.IO.Ports.SerialData.Chars)
                {
                    int bytesToRead = this.serialPort1.BytesToRead;
                    byte[] readBuffer = new byte[bytesToRead];
                    int bytesRead = this.serialPort1.Read(readBuffer, 0, bytesToRead);
                    this.BeginInvoke(new SerialDataReceived(SerialDataReceivedHandler), readBuffer);
                }
            }
            catch (Exception)
            {
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (commState)
            {
                case CommState.none:
                    break;
                case CommState.sentrc:
                    commtickcounter++;
                    if (commtickcounter >= 2000 / timer1.Interval)
                    {
                        commState = CommState.none;
                        MessageBox.Show("RC timeout");
                    }
                    break;
                case CommState.sentwc:
                    commtickcounter++;
                    if (commtickcounter >= 2000 / timer1.Interval)
                    {
                        commState = CommState.none;
                        MessageBox.Show("WC timeout");
                    }
                    break;
            }
            /*
            NULLState();
            PortClosedState();
            sentrcState();
            sentwcState();
            if(commtickcounter>=50)
            {
                MessageBox.Show("ERROR");
            }
            */
        }

        private void buttonWriteConf_Click_1(object sender, EventArgs e)
        {
            if (nud_startHeater.Value < 3)
            {
                commState = CommState.none;
                MessageBox.Show("Start heater temperature should be at least 3oC");
                return;
            }
            else if (nud_startHeater.Value > 40)
            {
                commState = CommState.none;
                MessageBox.Show("Start heater temperature should be at most 40oC");
                return;
            }
            else if (nud_stopHeater.Value < 3)
            {
                commState = CommState.none;
                MessageBox.Show("Stop heater temperature should be at least 3oC");
                return;
            }
            else if (nud_stopHeater.Value > 40)
            {
                commState = CommState.none;
                MessageBox.Show("Stop heater temperature should be at most 40oC");
                return;
            }
            else if (nud_fan_idle.Value < 15)
            {
                commState = CommState.none;
                MessageBox.Show("Idle fan PWM percentage should be at least 3oC");
                return;
            }
            else if (nud_fan_idle.Value > 100)
            {
                commState = CommState.none;
                MessageBox.Show("Idle fan PWM percentage should be at most 40oC");
                return;
            }
            else if (nud_fan_working.Value < 15)
            {
                commState = CommState.none;
                MessageBox.Show("Working fan PWM percentage should be at least 3oC");
            }
            else if (nud_fan_working.Value > 100)
            {
                commState = CommState.none;
                MessageBox.Show("Working fan PWM percentage should be at most 40oC");
                return;
            }
            else if (nud_startHeater.Value + 3 > nud_stopHeater.Value)
            {
                commState = CommState.none;
                MessageBox.Show("Stopping heater temperature must be at least 3 degrees more that starting heater temperature");
                return;
            }
            else if (nud_fan_idle.Value + 40 > nud_fan_working.Value)
            {
                commState = CommState.none;
                MessageBox.Show("Working fan PWM percentage must be at least 40% more than the idle fan PWM percentage");
                return;
            }
            else if (nud_delay.Value < 1000)
            {
                commState = CommState.none;
                MessageBox.Show("Delay should be at least 1000 msec");
                return;
            }
            else if (nud_delay.Value > 15000)
            {
                commState = CommState.none;
                MessageBox.Show("Delay should be at most 15000msec");
                return;
            }

            String stringToSend = nud_startHeater.Value.ToString() + " " + nud_stopHeater.Value.ToString() +
           " " + nud_fan_idle.Value.ToString() + " " + nud_fan_working.Value.ToString() + " " +
           nud_delay.Value.ToString();

            if (!this.serialPort1.IsOpen)
            {
                MessageBox.Show("Serial Port is closed");
                return;
            }

            string stringSender = "wc " + stringToSend + "\r";

            byte[] bytesToSend = Encoding.ASCII.GetBytes(stringSender);
            this.serialPort1.Write(bytesToSend, 0, bytesToSend.Length);
            commState = CommState.sentwc;
            commtickcounter = 0;
        }

        private void buttonReadFromFile_Click(object sender, EventArgs e)
        {

            this.openFileDialog1.Title = "Select a file with heater settings data to load";
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "Heater settings data|*.hsd|All files|*.* ";
            if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            // In order to have more details to error messages, we will read the file into a string and
            // then we will parse the string.
            if (!File.Exists(this.openFileDialog1.FileName))
            {
                MessageBox.Show(this, String.Format("The file {0} you defined does not exist.", this.openFileDialog1.FileName));
                return;
            }
            string xmlContents;
            try
            {
                FileInfo fi = new FileInfo(this.openFileDialog1.FileName);
                if (fi.Length > 3000000)
                {
                    MessageBox.Show(this, String.Format("The size of the file {0} is too big. The file cannot be loaded.", this.openFileDialog1.FileName));
                    return;
                }
                xmlContents = File.ReadAllText(this.openFileDialog1.FileName, new System.Text.UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                string message = String.Format(
                       " Error reading file { 0}. For more information, please see the file { 1}. The loading of the heater settings data file failed.", this.openFileDialog1.FileName);
                MessageBox.Show(message);
                return;
            }
            // We will parse the calibration data into a temporary variable.
            HeaterSettings hs = null;
            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(HeaterSettings));
                // By using XmlTextReader to read the file, any \r\n that may exist in strings, are handled
                // correctly (StreamReader does not handle \r\n pairs correctly).
                using (XmlTextReader xtr = new XmlTextReader(new StringReader(xmlContents)))
                {
                    hs = (HeaterSettings)xs.Deserialize(xtr);
                }
                // If we are here, we have no exception, so the data we read are (most probably) correct.
                // We will transfer the settings from the "hs" class to our other variables.
                nud_startHeater.Value = hs.startHeater;
                nud_stopHeater.Value = hs.stopHeater;
                nud_fan_idle.Value = hs.fan_idle;
                nud_fan_working.Value = hs.fan_working;
                nud_delay.Value = hs.delay;
                MessageBox.Show("Heater settings loaded successfully.");
            }
            catch (Exception ex)
            {
                string message = String.Format(
                       " An error occured while decoding the contents of the file { 0}. Maybe this file is not a heater settings data file. ",this.openFileDialog1.FileName);
                MessageBox.Show(message);

            }

        }

        private void buttonWriteToFile_Click(object sender, EventArgs e)
        {
            HeaterSettings hs = new HeaterSettings();
            hs.startHeater = (int) nud_startHeater.Value;
            hs.stopHeater = (int) nud_stopHeater.Value;
            hs.fan_idle = (int) nud_fan_idle.Value;
            hs.fan_working = (int) nud_fan_working.Value;
            hs.delay = (int) nud_delay.Value;


            try
            {
                this.saveFileDialog1.Title = " Enter a filename to save the heater settings data.";
                this.saveFileDialog1.DefaultExt = "hsd";
                this.saveFileDialog1.Filter = "Heater settings data|*.hsd|All files|*.* ";
                if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                // Blank filename warning.
                if (String.IsNullOrEmpty(Path.GetFileName(this.saveFileDialog1.FileName)))
                {
                    MessageBox.Show("Filename is blank. Cannot save");
                    return;
                }
                if (File.Exists(this.saveFileDialog1.FileName))
                {
                    // Possibly overwrite warning.
                }
                // Write to the file. We do the handling in two steps, first we open a StreamWriter
                // and then we write with a new XmlTextWriter(StreamWriter.BaseStream). In this way,
                // in case of error we can have more details about the error.
                System.IO.StreamWriter file;
                try
                {
                    file = new System.IO.StreamWriter(this.saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format(
                           " Error creating file{ 0}. The heater settings data are not saved. ",
                                         Path.GetFileName(this.saveFileDialog1.FileName));
                    MessageBox.Show(message);
                    return;
                }
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(HeaterSettings));
                // To write, we do not use the StreamWriter but the XmlTextWriter in correspondense
                // with the XmlTextReader that we use for reading. In order to have indented XML file
                // we set xtr.Formatting = Formatting.Indented. When we create the XmlTextWriter
                // we use a new System.Text.UTF8Encoding(true), so at the XML file there will be an initial
                // xml tag that states we have utf-8, but also, to place a Byte Order Mark EF BB BF
                // at the begining of the file.
                using (XmlTextWriter xtr = new XmlTextWriter(file.BaseStream, new System.Text.UTF8Encoding(true)))
                {
                    xtr.Formatting = Formatting.Indented;
                    xs.Serialize(xtr, hs);
                }
                // Εδώ αν καλέσουμε την file.Close(); παίρνουμε exception γιατί η using πιό πάνω έχει
                // κλείσει το file.BaseStream.


                hs.startHeater = (int) nud_startHeater.Value;
                hs.stopHeater = (int) nud_stopHeater.Value;
                hs.fan_idle = (int) nud_fan_idle.Value;
                hs.fan_working = (int) nud_fan_working.Value;
                hs.delay = (int) nud_delay.Value;
                MessageBox.Show("Heater settings saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving heater settings data.");
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbConnection1 != null)
            {
                try
                {
                    dbConnection1.Close();
                }
                catch (Exception)
                {
                }
                try
                {
                    dbConnection1.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
