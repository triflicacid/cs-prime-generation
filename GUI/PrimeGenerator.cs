using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PrimeNumberGeneratorApp
{
	public class PrimeNumberGenerator : Form
	{
		#region InitiateVars
		// Basics
		public FontFamily Verdana = new FontFamily("Verdana");
		public TextBox PrimeLimit;
		public Button btnGenPrimes;
		public Button btnViewPrimes;
		public Button btnRefresh;
		public Button btnSavePrimesToFile;
		public TextBox outputTextBox;
		public CheckBox showGenerationOutput;
		public ProgressBar LoadingBar;
		public CheckBox showProgress;
		
		public CheckBox doLogSession;
		public TextBox routineMultiplier;
		public Button startRoutine;
		
		// Outputs
		public long[] GeneratedPrimes;
		public long PrimeMaxValue;
		#endregion InitiateVars
		
		#region Constructor
		public PrimeNumberGenerator()
		{
			// Load display
			InitComponents();
		}
		#endregion Constructor
		
		#region InitComponents
		public void InitComponents()
		{
			this.AutoScaleDimensions = new SizeF(16, 13);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(700, 600);
			this.Name = "Form1";
			this.Text = "Prime Number Generator";
			
			// Visual "Title" (label)
			Label label_title = new Label();
			label_title.Font = new Font(Verdana, 16, FontStyle.Bold | FontStyle.Underline);
			label_title.Location = new Point(76, 25);
			label_title.Size = new Size(320, 25);
			label_title.Text = "Prime Number Generator";
			label_title.TextAlign = ContentAlignment.MiddleCenter;
			this.Controls.Add(label_title);
			
			// Label for prime max number
			Label label_prime_max = new Label();
			label_prime_max.Font = new Font(Verdana, 8.5F, FontStyle.Italic);
			label_prime_max.Location = new Point(50, 78);
			label_prime_max.Size = new Size(90, 13);
			label_prime_max.Text = "Upper Limit: ";
			this.Controls.Add(label_prime_max);
			
			// Text box for prime limit
			PrimeLimit = new TextBox();
			PrimeLimit.Location = new Point(140, 75);
			PrimeLimit.Size = new Size(140, 20);
			PrimeLimit.MaxLength = 19;
			this.Controls.Add(PrimeLimit);
			
			// Button for generating primes
			btnGenPrimes = new Button();
			btnGenPrimes.Location = new Point(300, 74);
			btnGenPrimes.Size = new Size(110, 23);
			btnGenPrimes.Text = "Generate Primes";
			btnGenPrimes.Click += new EventHandler(btnGenPrimes_Click);
			this.Controls.Add(btnGenPrimes);
			
			// Checkbox - enable/disable logging session
			doLogSession = new CheckBox();
			doLogSession.Location = new Point(450, 74);
			doLogSession.Size = new Size(200, 23);
			doLogSession.Text = "Enable Routine Generation";
			doLogSession.Font = new Font(Verdana, 8);
			//doLogSession.Click += new EventHandler(btnGenPrimes_Click);
			this.Controls.Add(doLogSession);
			
			// Progress bar
			LoadingBar = new ProgressBar();
			LoadingBar.Location = new Point(50, 105);
			LoadingBar.Width = 360;
			LoadingBar.Height = 15;
			LoadingBar.Minimum = 0;
			LoadingBar.Maximum = 100;
			LoadingBar.Step = 1;
			LoadingBar.Value = 0;
			this.Controls.Add(LoadingBar);
			
			// Label for routine multiplier
			Label routineMultLabel = new Label();
			routineMultLabel.Location = new Point(450, 105);
			routineMultLabel.Size = new Size(115, 23);
			routineMultLabel.Text = "Iteration multiplier: ";
			routineMultLabel.Font = new Font(Verdana, 7.3F);
			this.Controls.Add(routineMultLabel);
			
			// Text box for routine multiplier
			routineMultiplier = new TextBox();
			routineMultiplier.Location = new Point(570, 103);
			routineMultiplier.Size = new Size(50, 20);
			routineMultiplier.MaxLength = 5;
			this.Controls.Add(routineMultiplier);
			
			// Button for refreshing form
			btnRefresh = new Button();
			btnRefresh.Location = new Point(50, 133);
			btnRefresh.Size = new Size(110, 23);
			btnRefresh.Text = "Refresh";
			btnRefresh.Click += new EventHandler(btnRefresh_Click);
			this.Controls.Add(btnRefresh);
			
			// Button for viewing primes
			btnViewPrimes = new Button();
			btnViewPrimes.Location = new Point(175, 133);
			btnViewPrimes.Size = new Size(110, 23);
			btnViewPrimes.Text = "View Primes";
			btnViewPrimes.Enabled = false;
			btnViewPrimes.Click += new EventHandler(btnViewPrimes_Click);
			this.Controls.Add(btnViewPrimes);
			
			// Save primes to file Button
			btnSavePrimesToFile = new Button();
			btnSavePrimesToFile.Location = new Point(300, 133);
			btnSavePrimesToFile.Size = new Size(110, 23);
			btnSavePrimesToFile.Text = "Save Primes To File";
			btnSavePrimesToFile.Font = new Font(Verdana, 7);
			btnSavePrimesToFile.Enabled = false;
			btnSavePrimesToFile.Click += new EventHandler(btnSavePrimesToFile_Click);
			this.Controls.Add(btnSavePrimesToFile);

			// Button to start routine
			startRoutine = new Button();
			startRoutine.Location = new Point(450, 133);
			startRoutine.Size = new Size(170, 23);
			startRoutine.Text = "Start Routine Generation";
			startRoutine.Font = new Font(Verdana, 8);
			startRoutine.Enabled = false;
			//startRoutine.Click += new EventHandler(startRoutine_Click);
			this.Controls.Add(startRoutine);
			
			// Checkbox whether to show progress (LoadingBar)
			showProgress = new CheckBox();
			showProgress.Font = new Font(Verdana, 10);
			showProgress.Text = "Show Progress ";
			showProgress.Size = new Size(120, 23);
			showProgress.Location = new Point(50, 175);
			showProgress.Font = new Font(Verdana, 8);
			showProgress.Click += new EventHandler(showProgress_Click);
			this.Controls.Add(showProgress);
			
			// Checkbox whether to show generation output
			showGenerationOutput = new CheckBox();
			showGenerationOutput.Font = new Font(Verdana, 10);
			showGenerationOutput.Text = "Show working ";
			showGenerationOutput.Size = new Size(125, 23);
			showGenerationOutput.Location = new Point(175, 175);
			showGenerationOutput.Font = new Font(Verdana, 8);
			showGenerationOutput.Click += new EventHandler(showGenerationOutput_Click);
			this.Controls.Add(showGenerationOutput);
			
			// Big output text box
			outputTextBox = new TextBox();
			outputTextBox.Location = new Point(51, 205);
			outputTextBox.Multiline = true;
			outputTextBox.WordWrap = false;
			outputTextBox.Size = new Size(600, 370);
			outputTextBox.ReadOnly = true;
			outputTextBox.ScrollBars = ScrollBars.Both;
			this.Controls.Add(outputTextBox);
		}
		#endregion InitComponents
		
		#region checkboxes click handlers
		private void showGenerationOutput_Click(object sender, EventArgs e)
		{
			if (showGenerationOutput.Checked)
			{
				// Warn user about execution decrement
				DialogResult choice = MessageBox.Show("Enabling working will have a negative impact on generation time."+Environment.NewLine+"Enable working anyway?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (choice == DialogResult.No)
					showGenerationOutput.Checked = false;
			}
		}
		
		private void showProgress_Click(object sender, EventArgs e)
		{
			if (showProgress.Checked)
			{
				// Warn user about execution decrement
				DialogResult choice = MessageBox.Show("Enabling the progress meter will have a negative impact on generation time."+Environment.NewLine+"Enable progress meter anyway?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (choice == DialogResult.No)
					showProgress.Checked = false;
			}
		}
		
		#endregion checkboxes click handlers
		
		#region Other Helpers
		private void maxValueValueError()
		{
			MessageBox.Show("Max Number is required to be a number in range 0-9,223,372,036,854,775,807", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		#endregion Other Helpers

		#region ViewPrimes OnClick
		private void btnViewPrimes_Click(object sender, EventArgs e)
		{
			// Clear output box
			outputTextBox.Clear();
			
			// If length is substantial, give message
			if (GeneratedPrimes.Length > 10000)
				MessageBox.Show("Due to the number of primes, this may take a while...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			outputTextBox.Text = String.Join(", ", GeneratedPrimes);
		}
		#endregion ViewPrimes OnClick
		
		#region GenPrimes OnClick
		private void btnGenPrimes_Click(object sender, EventArgs e)
		{
			// Initiate (clear progress bar and output)
			outputTextBox.Clear();
			LoadingBar.Value = 0;
			
			// Check for max value. And check if correct data type
			try
			{
				long maxValue = Convert.ToInt64(PrimeLimit.Text);
				if (maxValue < 1)
				{
					maxValueValueError();
					return;
				}
				PrimeMaxValue = maxValue;
				
				// Generate
				Text = "Prime Number Generator | Generating Primes 2-"+maxValue+"...";
				outputTextBox.Text += "Generating primes in range 2-" + maxValue + "...";
				if (showGenerationOutput.Checked)
					outputTextBox.Text += Environment.NewLine;
				
				long time_start = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
				long[] primes = Generate(maxValue, showGenerationOutput.Checked, showProgress.Checked);
				long time_end = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
				
				GeneratedPrimes = primes;
				btnViewPrimes.Enabled = true;
				btnSavePrimesToFile.Enabled = true;
				
				if (showGenerationOutput.Checked)
					outputTextBox.Text += Environment.NewLine;
				outputTextBox.Text += "  --- Done ---  " + Environment.NewLine;
				outputTextBox.Text += "Time ellapsed: " + (time_end-time_start)+" ms" + Environment.NewLine;
				outputTextBox.Text += "Primes found: "+primes.Length + Environment.NewLine;
				Text = "Prime Number Generator | Complete. Found "+primes.Length+" primes";
				
				MessageBox.Show("Prime generation complete: found " + primes.Length + " primes [" + (time_end-time_start) + " ms ellapsed]", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (OverflowException)
			{
				maxValueValueError();
				return;
			}
			catch (Exception error)
			{
				MessageBox.Show("An internal error occured. Please contact the developer."+Environment.NewLine+Environment.NewLine+"Error: "+error.Message, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Console.WriteLine(error);
				return;
			}
		}
		#endregion GenPrimes OnClick
		
		#region Refresh OnClick
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			outputTextBox.Clear();
			LoadingBar.Value = 0;
			PrimeLimit.Text = "";
			Text = "Prime Number Generator";
			GeneratedPrimes = new long[0];
			PrimeMaxValue = 0;
			btnViewPrimes.Enabled = false;
			btnSavePrimesToFile.Enabled = false;
			showProgress.Checked = false;
			showGenerationOutput.Checked = false;
		}
		#endregion Refresh OnClick
		
		#region Func GeneratePrimes
		public long[] Generate(long limit, bool output = false, bool doLoadingBar = false)
		{	
			if (doLoadingBar)
				this.LoadingBar.Maximum = Convert.ToInt32(Math.Sqrt(limit)) + 1;
			string outputText = String.Empty;

			// Create marks array
			// -> False: Prime
			// -> True: Not Prime
			bool[] marks = new bool[limit + 1];

			// NB: If not "output" or "doLoadingBar", do this version (it omits IF statements: quicker)
			if (!doLoadingBar && !output)
			{
				// Loop through each value
				for (long i = 2; i * i <= limit; i += 1)
				{
					// If is prime...
					if (!marks[i])
					{
						// Mark off all multiples as non-prime
						for (long j = i * i; j <= limit; j += i)
							marks[j] = true;
					}
					
					// Update loading bar
					if (doLoadingBar)
					{
						this.LoadingBar.PerformStep();
						this.Refresh();
					}
				}
			}
			else
				// Do output stuff
			{
				// Loop through each value
				for (long i = 2; i * i <= limit; i += 1)
				{
					if (output)
						outputText += "Current Prime: " + i + ":" + Environment.NewLine;
					// If is prime...
					if (!marks[i])
					{
						if (output)
							outputText += "  > Removing: ";
						// Mark off all multiples as non-prime
						for (long j = i * i; j <= limit; j += i)
						{
							marks[j] = true;
							if (output)
								outputText += j + ", ";
						}
						if (output)
							outputText += Environment.NewLine;
					}
					
					// Update loading bar
					if (doLoadingBar)
					{
						this.LoadingBar.PerformStep();
						this.LoadingBar.Refresh();
					}
				}
			}

			// Find number of prime numbers
			// NB: Exclude (0, 1) as not prime, so start count @ -2
			long primeCount = -2;
			foreach (bool mark in marks)
			{
				if (!mark) primeCount += 1;
			}
			// Array containing prime numbers
			long[] primes = new long[primeCount];
			long primeIndex = 0;

			// Fill array with all marked 'False' && > 1: these are prime (0, 1 arent prime)
			for (long i = 0; i <= limit; i += 1)
			{
				if (i > 1 && !marks[i])
				{
					primes[primeIndex] = i;
					primeIndex += 1;
				}
			}
			
			if (doLoadingBar)
				LoadingBar.Value = LoadingBar.Maximum;
			if (output)
				outputTextBox.Text += outputText;
			
			return primes;
		}
		#endregion Func GeneratePrimes
		
		#region Func WritePrimesToFile
		public void WritePrimesToFile()
		{
			string filename = "primesTo" + PrimeMaxValue + "." + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() + ".txt";
			
			File.WriteAllText(filename, String.Join(", ", GeneratedPrimes));
			
			MessageBox.Show("Prime numbers saved to file " + filename, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion Func WritePrimesToFile
		
		#region btnSavePrimesToFile Onclick
		private void btnSavePrimesToFile_Click(object sender, EventArgs e)
		{
			WritePrimesToFile();
		}
		#endregion SavePrimesToFile Onclick
		
		#region EntryPoint
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.Run(new PrimeNumberGenerator());
		}
		#endregion EntryPoint
	}
}
