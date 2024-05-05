using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech;
using System.IO;

namespace TextToSpeechDemo
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speech;
        public Form1()
        {
            InitializeComponent();
            speech = new SpeechSynthesizer();
        }
        // Original code by: Original code by: C# Ui Academy
        // https://www.youtube.com/watch?v=aGtKyZ4AFcw

        // Alerts, file saving, button to clear the text box, and voice selection added by MrBones (Me).
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var voice in speech.GetInstalledVoices())
            {
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                speech.SelectVoice(comboBox1.Text);
                speech.SpeakAsync(richTextBox1.Text);
                this.Alert("Speaking", Form_Alert.enmType.Success);
            }
            else
            {
                this.Alert("Pls write something", Form_Alert.enmType.Error);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Speaking)
            {
                speech.Pause();
                this.Alert("Paused", Form_Alert.enmType.Warning);
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Paused)
            {
                speech.Resume();
                this.Alert("Continuing", Form_Alert.enmType.Info);
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Wave File|*.wav";
            saveFileDialog.Title = "Save Speech as Wave File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                speech.SetOutputToWaveFile(saveFileDialog.FileName);
                speech.Speak(richTextBox1.Text);
                speech.SetOutputToDefaultAudioDevice();
                this.Alert("Saved!", Form_Alert.enmType.Info);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            this.Alert("Text Box Cleaned", Form_Alert.enmType.Info);
        }
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
    }
}
