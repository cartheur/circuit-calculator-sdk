using System.Data.SQLite;

namespace Circuit.Data
{
    public class ExperimentalData
    {
        private static bool instance = false;
        private static string sessionID = (string)AppDomain.CurrentDomain.GetData("SessionID");
        private static string frequencyUnit = "Hz";

        public ExperimentalData()
        {
            instance = true;
            //sessionValue.Text = (string)AppDomain.CurrentDomain.GetData("SessionID");

        }
        /// <summary>
        /// Checks the instance of the form.
        /// </summary>
        public static bool Instance { get { return instance; } set { instance = value; } }
        /// <summary>
        /// Stores the input experimental data.
        /// </summary>
        private void storeOscillatorData_Click(object sender, EventArgs e)
        {
            double resonanceFrequency = 0;
            string experimentBatchNumber = "0";

            if (experimentBatchSelection.SelectedIndex == -1 | measuredResonanceFrequencyInputBox.Text == "")
                MessageBox.Show("You must first select an experiment batch and enter a resonance frequency to utilize storage.", "Data Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            else
            {
                double tempResonanceFrequency = Convert.ToDouble(measuredResonanceFrequencyInputBox.Text);

                if (frequencyUnit.Text == "MHz")
                {
                    resonanceFrequency = tempResonanceFrequency * 1E6;
                }
                if (frequencyUnit.Text == "kHz")
                {
                    resonanceFrequency = tempResonanceFrequency * 1E3;
                }
                if (frequencyUnit.Text == "Hz")
                {
                    resonanceFrequency = tempResonanceFrequency;
                }
                experimentBatchNumber = experimentBatchSelection.SelectedItem.ToString();

                StoreData(experimentBatchNumber, resonanceFrequency);
            }

        }
        /// <summary>
        /// Stores the data from the experiment into the database.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <param name="frequency">The frequency.</param>
        protected void StoreData(string batchNumber, double frequency)
        {
            try
            {
                // Store the session ID, batch, and frequency.
                string directory = MapPath(@"\db\sessions.db");
                string path = @"c:\Users\ctucker\aeonProjectDirectory\Monkey-Man\thesis\circuits\CircuitCalculator";

                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + path + directory);
                SQLiteCommand cmd = new SQLiteCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO ExperimentalResonanceData (SessionID, ExperimentBatch, MeasuredResonanceFrequency) VALUES ('" + sessionValue.Text + "', '" + batchNumber + "', '" + frequency + "')";
                conn.Open();
                SQLiteTransaction trans = conn.BeginTransaction();
                cmd.ExecuteNonQuery();
                trans.Commit();
                conn.Close();
                trans.Dispose();
                cmd.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                MessageBox.Show("Experiment saved succesfully.", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            
        }
        /// <summary>
        /// Maps the path of the executing file.
        /// </summary>
        /// <param name="path">The path to map.</param>
        public virtual string MapPath(string path)
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string zebra = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string local = Environment.CurrentDirectory;
            return Path.Combine(zebra, path);
        }
    }
}
