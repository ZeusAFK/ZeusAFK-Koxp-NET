using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace ZeusAFK_koxp.NET
{
    public partial class Form1 : Methods
    {

        public Form1()
        {
            InitializeComponent();
        }

        public string Job;
        public int[] PartyLifes = new int[8];
        public int MyLife = 0;
        public string LastMaliced = "";

        public string[,] cSkills;

        public void MakeAttack(string skill)
        {

            if (!(FormatDec(EnemyID(), 4) <= 9999))
            {
                if (chkRunToMob.Checked && chkSpeedHackTo.Checked && CharX() != EnemyX() && CharY() != EnemyY())
                    SpeedHackTo(EnemyX(), EnemyY());
                else if (chkRunToMob.Checked && CharX() != EnemyX() && CharY() != EnemyY())
                    GoTo(EnemyX(), EnemyY());

                if (CharJob().Equals("Rogue"))
                    RogueAttack(skill, chkR.Checked);
                if (CharJob().Equals("Warrior"))
                    WarriorAttack(skill, chkR.Checked);
                if (CharJob().Equals("Priest"))
                    PriestAttack(skill, chkR.Checked);
                if (CharJob().Equals("Mage"))
                    MageAttack(skill, chkR.Checked);
            }
        }

        public int GetSkillIndex(string Skill)
        {
            for (int i = 0; i < cSkills.GetLength(0); i++)
                if (cSkills[i, 0].Equals(Skill))
                    return i;
            return -1;
        }

        public string ChoosenSkill()
        {
            int Choosen = -1; int Delay = -1;
            for (int i = 0; i < lstSkills.Items.Count; i++)
                if (lstSkills.GetItemChecked(i))
                {
                    int SkillIndex = GetSkillIndex(lstSkills.Items[i].ToString());
                    if (int.Parse(cSkills[SkillIndex, 2]) >= int.Parse(cSkills[SkillIndex, 1]) && int.Parse(cSkills[SkillIndex, 2]) >= Delay)
                    {
                        Choosen = i;
                        Delay = int.Parse(cSkills[SkillIndex, 2]);
                    }
                    cSkills[SkillIndex, 2] = (int.Parse(cSkills[SkillIndex, 2]) + 1).ToString();
                }
            if (!EnemyID().Equals("FFFF") && EnemyHP() != 0 && EnemyDistance() < AttackDistance.Value)
                try { cSkills[GetSkillIndex(lstSkills.Items[Choosen].ToString()), 2] = "0"; }
                catch { return ""; }
            try { return lstSkills.Items[Choosen].ToString(); }
            catch { return ""; }
        }

        public void StartKoxp()
        {
            AddressPointer = ToInt32(ReadMemory(new IntPtr(PTR_CHR)));
            LoadControls();
            SND_FIX();
            tmrInformacion.Enabled = true;
            System.Media.SystemSounds.Asterisk.Play();
            TimerTSkills.Enabled = true;
            if (!System.IO.Directory.Exists(Application.StartupPath + "\\logs"))           
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\logs");
            //System.IO.File.Create(Application.StartupPath + "\\logs\\" + CharName() + ".log");
            writer = new System.IO.StreamWriter(System.IO.File.Create(Application.StartupPath + "\\logs\\" + CharName() + ".log"));
        }

        public void LoadControls()
        {
            if (CharJob().Equals("Rogue"))
            {
                lstSkills.Items.Clear();
                cSkills = new string[,]
                {
                    {"Stab","6","6"},
                    {"Stab2","6","6"},
                    {"Jab","6","6"},
                    {"Pierce","11","11"},
                    {"Shock","6","6"},
                    {"Thrust","11","11"},
                    {"Cut","6","6"},
                    {"Spike","12","12"},
                    {"Blody Beast","6","6"},
                    {"Blinding","60","60"},
                    {"Archery","0","0"},
                    {"Through Shot","0","0"},
                    {"Fire Arrow","3","3"},
                    {"Poison Arrow","3","3"},
                    {"Multiple Shot","0","0"},
                    {"Guided Arrow","0","0"},
                    {"Perfect Shot","0","0"},
                    {"Fire Shot","4","4"},
                    {"Poison Shot","4","4"},
                    {"Arc Shot","0","0"},
                    {"Explosive Shot","4","4"},
                    {"Counter Strike","60","60"},
                    {"Arrow Shower","0","0"},
                    {"Shadow Shot","0","0"},
                    {"Shadow Hunter","0","0"},
                    {"Ice Shot","6","6"},
                    {"Lightning Shot","6","6"},
                    {"Dark Pursuer","0","0"},
                    {"Blow Arrow","0","0"},
                    {"Blinding Strafe","60","60"},
                    {"Super Archer","0","0"}
                };

                lstTimedSkills.Items.Clear();
                lstTimedSkills.Items.Add("Wolf");
                lstTimedSkills.Items.Add("Swift");
                lstTimedSkills.Items.Add("Light Feet");
                lstTimedSkills.Items.Add("Evade");
                lstTimedSkills.Items.Add("Safely");
                lstTimedSkills.Items.Add("Skaled Skin");
                lstTimedSkills.Items.Add("Lupine Eyes");
                lstTimedSkills.Items.Add("Hide");
            }

            if (CharJob().Equals("Warrior"))
            {
                lstSkills.Items.Clear();
                cSkills = new string[,]
                {
                    {"Slash","3","3"},
                    {"Crash","3","3"},
                    {"Piercing","3","3"},
                    {"Hash","3","3"},
                    {"Hoodwink","0","0"},
                    {"Shear","3","3"},
                    {"Pierce","0","0"},
                    {"Carwing","0","0"},
                    {"Sever","3","3"},
                    {"Prick","0","0"},
                    {"Multiple Shock","3","3"},
                    {"Cleave","0","0"},
                    {"Mangling","3","3"},
                    {"Thrust","0","0"},
                    {"Sword Aura","0","0"},
                    {"Sword Dancing","0","0"},
                    {"Howling Sword","1","1"},
                    {"Blooding","21","21"},
                    {"Hell Blade","1","1"}
                };
                lstTimedSkills.Items.Clear();
                lstTimedSkills.Items.Add("Gain");
                lstTimedSkills.Items.Add("Defense");
                lstTimedSkills.Items.Add("Sprint");
            }

            if(CharJob().Equals("Priest"))
            {
                lstSkills.Items.Clear();
                cSkills = new string[,]
                {
                    {"Stroke","0","0"},
                    {"Holy Attack","0","0"},
                    {"Wrath","0","0"},
                    {"Wield","0","0"},
                    {"Harsh","2","2"},
                    {"Collapse","3","3"},
                    {"Collision","0","0"},
                    {"Shuddering","0","0"},
                    {"Ruin","2","2"},
                    {"Hellish","3","3"},
                    {"Tilt","0","0"},
                    {"Bloody","0","0"},
                    {"Raving Edge","2","2"},
                    {"Hades","3","3"},
                    {"Judgment","0","0"},
                    {"Helis","0","0"}
                };
                lstTimedSkills.Items.Clear();
                lstTimedSkills.Items.Add("Strength");
                lstTimedSkills.Items.Add("Prayer Of Cronos");
                lstTimedSkills.Items.Add("Prayer Of God's Power");
                lstTimedSkills.Items.Add("Blasting");
                lstTimedSkills.Items.Add("Wildness");
            }
            if (CharJob().Equals("Mage"))
            {
                lstSkills.Items.Clear();
                cSkills = new string[,]
                {
                    {"Counter Spell","6","6"},
                    {"Lightining","5","5"},
                    {"Static Hemispher","1","1"},
                    {"Thunder","5","5"},
                    {"Thunder Blast","5","5"},
                    {"Charged Blade","1","1"},
                    {"Specter Of Thunder","1","1"},
                    {"Static Orb","6","6"},
                    {"Static Thorn","6","6"},
                    {"Manes Of Thunder","1","1"},
                    {"Thunder Impact","21","21"},
                    {"Burn","1","1"},
                    {"Ignition","1","1"},
                    {"Specter Of Fire","1","1"},
                    {"Manes Of Fire","1","1"},
                    {"Fire Blast","5","5"},
                    {"Blaze","6","6"},
                    {"Fire Spear","5","5"},
                    {"Hell Fire","5","5"},
                    {"Pillar Of Fire","6","6"},
                    {"Fire Thorn","6","6"},
                    {"Fire Impact","21","21"},
                    {"Fire Ball","5","5"},
                    {"Freeze","1","1"},
                    {"Chill","6","6"},
                    {"Solid","1","1"},
                    {"Frostbite","5","5"},
                    {"Frozen Blade","1","1"},
                    {"Specter Of Ice","1","1"},
                    {"Ice Comet","6","6"},
                    {"Ice Orb","5","5"},
                    {"Manes Of Ice","1","1"},
                    {"Ice Impact","21","21"},
                    {"Ice Staff","1","1"},
                    {"Flame Staff","1","1"},
                    {"Glacier Staff","1","1"},
                    {"Lightining Staff","1","1"},
                    {"Inferno","16","16"},
                    {"Blizzard","16","16"},
                    {"Thundercloud","16","16"},
                    {"Super Nova","16","16"},
                    {"Frost Nova","16","16"},
                    {"Static Nova","16","16"},
                    {"Chain Lightning","19","19"},
                    {"Ice Storm","19","19"},
                    {"Meteor Fall","19","19"},
                    {"Incineration", "22", "22"}
                };
            }

            TSkills[0, 0] = "Wolf"; TSkills[0, 1] = "120"; TSkills[0, 2] = "120";
            TSkills[1, 0] = "Swift"; TSkills[1, 1] = "600"; TSkills[1, 2] = "600";
            TSkills[2, 0] = "Light Feet"; TSkills[2, 1] = "10"; TSkills[2, 2] = "10";
            TSkills[3, 0] = "Evade"; TSkills[3, 1] = "30"; TSkills[3, 2] = "30";
            TSkills[4, 0] = "Safely"; TSkills[4, 1] = "30"; TSkills[4, 2] = "30";
            TSkills[5, 0] = "Skaled Skin"; TSkills[5, 1] = "30"; TSkills[5, 2] = "30";
            TSkills[6, 0] = "Lupin Eyes"; TSkills[6, 1] = "50"; TSkills[6, 2] = "50";
            TSkills[7, 0] = "Hide"; TSkills[7, 1] = "40"; TSkills[7, 2] = "40";

            TSkills[8, 0] = "Gain"; TSkills[8, 1] = "300"; TSkills[8, 2] = "300";
            TSkills[9, 0] = "Defense"; TSkills[9, 1] = "15"; TSkills[9, 2] = "15";
            TSkills[10, 0] = "Sprint"; TSkills[10, 1] = "10"; TSkills[10, 2] = "10";

            TSkills[11, 0] = "Strenght"; TSkills[11, 1] = "600"; TSkills[11, 2] = "600";
            TSkills[12, 0] = "Player Of Cronos"; TSkills[12, 1] = "100"; TSkills[12, 2] = "100";
            TSkills[13, 0] = "Prayer Of God's Power"; TSkills[13, 1] = "100"; TSkills[13, 2] = "100";
            TSkills[14, 0] = "Blasting"; TSkills[14, 1] = "400"; TSkills[14, 2] = "400";
            TSkills[15, 0] = "Wildness"; TSkills[15, 1] = "400"; TSkills[15, 2] = "400";
        }

        void ConfigAttack()
        {
            int Delay;
            try { Delay = int.Parse(txtDelay.Text); }
            catch { Delay = 1500; }

            if (CharJob().Equals("Rogue") && Job.Equals("Archer"))
                if (lstSkills.Text.Equals("Super Archer"))
                    txtDelay.Text = "2000";
                else
                    if (Delay < 1300)
                        txtDelay.Text = "1300";

            if (CharJob().Equals("Warrior") || CharJob().Equals("Priest"))
                if (Delay < 1300)
                    txtDelay.Text = "1300";

            if (CharJob().Equals("Mage"))
                if (Delay < 1000)
                    txtDelay.Text = "1000";

            timerAttack.Interval = int.Parse(txtDelay.Text);

            if (btnAttack.Text.Equals("Atacar"))
            {
                timerAttack.Enabled = true;
                btnAttack.Text = "Detener";
            }
            else
            {
                timerAttack.Enabled = false;
                btnAttack.Text = "Atacar";
            }
        }

        public bool CanAttack(string Enemy)
        {
            for (int i = 0; i < lstSlot.Items.Count; i++)
                if (lstSlot.Items[i].ToString().Equals(Enemy))
                    return true;
            return false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (AttachProccess(txtWindowsName.Text))
                StartKoxp();
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            ConfigAttack();
        }

        private void chkWallHack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWallHack.Checked)
                WriteMemory(new IntPtr(AddressPointer + OFF_WH), 0);
            else
                WriteMemory(new IntPtr(AddressPointer + OFF_WH), 1);
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelect.Checked)
            {
                timerSelect.Enabled = true;
                chkSelectDie.Enabled = true;
            }
            else
            {
                timerSelect.Enabled = false;
                chkSelectDie.Enabled = false;
            }
        }

        private void ScrollMinor_Scroll(object sender, ScrollEventArgs e)
        {
            timerMinor.Interval = ScrollMinor.Value;
        }

        private void btnSlotAdd_Click(object sender, EventArgs e)
        {
            lstSlot.Items.Add(EnemyName());
        }

        private void btnSlotRemove_Click(object sender, EventArgs e)
        {
            lstSlot.Items.RemoveAt(lstSlot.SelectedIndex);
        }

        private void btnSlotClear_Click(object sender, EventArgs e)
        {
            lstSlot.Items.Clear();
        }

        private void timerSelect_Tick(object sender, EventArgs e)
        {
            if (chkSelect.Checked && !chkSelectDie.Checked)
                SelectMob();
            if (chkSelect.Checked && chkSelectDie.Checked && (EnemyID().Equals("FFFF") || EnemyHP() == 0 || EnemyDistance() >= AttackDistance.Value || FormatDec(EnemyID(), 4) <= 9999))
                SelectMob();
        }

        private void timerAttack_Tick(object sender, EventArgs e)
        {
            string skill = ChoosenSkill();
            if (!EnemyID().Equals("FFFF") && EnemyDistance() < AttackDistance.Value)
            {
                if (chkSlot.Checked && CanAttack(EnemyName()))
                    MakeAttack(skill);
                else if (!chkSlot.Checked)
                    MakeAttack(skill);
            }
        }

        private void timerPotions_Tick(object sender, EventArgs e)
        {
            try
            {
                if (chkHP.Checked && CharHP() < ((CharMaxHP() * int.Parse(txtHP.Text)) / 100))
                    PotionHP(cmbHP.Text);

                if (chkMP.Checked && CharMP() < ((CharMaxMP() * int.Parse(txtMP.Text)) / 100))
                    PotionMP(cmbMP.Text);

                gridParty.Rows.Clear();
                gridParty.RowCount = 8;
                for (int i = 1; i <= PartyMembers(); i++)
                {
                    gridParty[0, i - 1].Value = PartyName(i);
                    gridParty[1, i - 1].Value = PartyHP(i);
                    gridParty[2, i - 1].Value = PartyMaxHP(i);
                    gridParty[3, i - 1].Value = PartyLevel(i);
                }
            }
            catch { }
        }

        private void timerMinor_Tick(object sender, EventArgs e)
        {
            if (chkMinor.Checked && CharHP() < ((CharMaxHP() * int.Parse(txtMinor.Text)) / 100))
                MinorHealing();
        }

        private void tmrInformacion_Tick(object sender, EventArgs e)
        {
            try
            {
                HPbar.Value = (CharHP() * 100) / CharMaxHP();
                MPbar.Value = (CharMP() * 100) / CharMaxMP();
                EXPbar.Value = Convert.ToInt32((CharExp() * 100) / CharMaxExp());
                if (chkSpeedHack.Checked)
                    SpeedHack();

                if (chkEscape.Checked && CharHP() < ((CharMaxHP() * int.Parse(txtHPescape.Text)) / 100))
                {
                    Packet("290103");
                    Packet("1200");
                }

                if (chkNotifyNoah.Checked)
                {
                    if (txtMail.Text.Trim().Length > 0 && txtMail.Text.IndexOf('@') != -1)
                        NotifyMoney(txtMail.Text.Trim());
                }

                if (chkAutoParty.Checked && PartyMembers() == 0)
                    Packet("2F0201");
            }
            catch { }
        }

        private void ScrollTransparency_Scroll(object sender, ScrollEventArgs e)
        {
            this.Opacity = ((double)ScrollTransparency.Value) / 100.0;
        }

        private void chkAllwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllwaysOnTop.Checked)
                SetWindowPos(new IntPtr(this.Handle.ToInt32()), new IntPtr(-1), this.Left, this.Top, this.Width, this.Height, (uint)0x2);
            else
                SetWindowPos(new IntPtr(this.Handle.ToInt32()), new IntPtr(-2), this.Left, this.Top, this.Width, this.Height, (uint)0x2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteMemory(new IntPtr(AddressPointer + 0xD29), 1);
            //Convert.ToInt32(BitConverter.ToSingle(ReadMemory(new IntPtr(Convert.ToInt32(PlayerBase(PartyID(Member)), 16) + OFF_Y)), 0));
            //MessageBox.Show("Enemy distance: " + EnemyDistance().ToString() + " Enemy ID: " + EnemyID().ToString() + " Enemy HP: " + EnemyHP().ToString() + "\n" + "Char X: " + CharX() + " Char Y: " + CharY() + "\n" + "Enemy X: " + EnemyX() + " Enemy Y: " + EnemyY());
            //string output = "";
            //for (int i = 170; i < 220; i += 2)
            //    output += "PartyX(" + i + "): " + Convert.ToInt32(BitConverter.ToSingle(ReadMemory(new IntPtr(Convert.ToInt32(PlayerBase(PartyID(1)), 16) + i)), 0)) +"\n";
            //MessageBox.Show(output);
            //MessageBox.Show(EnemyID());
            //Packet("2001" + "034B" + "BFFFFFFFF");
            //Packet("64046A080000");
            //Packet("64076A080000");
            //Packet("55000F31343433305F5465696C732E6C7561FF");
            //SendSkill(503, Convert.ToInt32(EnemyID(), 16));
            //string result = "";
            //for (int i = -50; i <= 50; i++)
            //    result += " " + CharServer(i) + " " + i.ToString();
            //MessageBox.Show(result);
            //MessageBox.Show(Convert.ToInt32(BitConverter.ToSingle(ReadMemory(new IntPtr(Convert.ToInt32(PlayerBase(PartyID(1)), 16) + 0xA28)), 0)).ToString());
            //MessageBox.Show(CharName());
            //SpeedHackTo(EnemyX(), EnemyY());
            //MessageBox.Show("" + Distance(CharX(), CharY(), EnemyX(), EnemyY()) + " " + EnemyDistance());
            //MessageBox.Show(PartyX(2).ToString() + " " + PartyY(2).ToString());
            //GoTo(EnemyX(), EnemyY());
            //MessageBox.Show(PlayerBase(EnemyID()));
            //WriteMemory(new IntPtr(AddressPointer + 0x696), 16256);
            //WriteMemory(new IntPtr(AddressPointer + 0x696), ToInt32(ReadMemory(new IntPtr(AddressPointer + 0x696))) + 400);
            //MessageBox.Show(ToInt32(ReadMemory(new IntPtr(AddressPointer + 0x696))).ToString());
            //MessageBox.Show(ToInt32(ReadMemory(new IntPtr(AddressPointer + 0xBC))).ToString());
            //WriteMemory(new IntPtr(AddressPointer + 0xBC), int.Parse(textBox1.Text));
            //int hook;
            //byte[] pBytes = new byte[25];
            //hook = BitConverter.ToInt32(ReadMemory(new IntPtr(PTR_DLG + 0x84), pBytes, 1L), 0);
            //MessageBox.Show(hook.ToString());
            /*
            string result = "byte[] connection = {";
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            byte[] connection = encoder.GetBytes("Server=70.87.28.217;Database=developer;Uid=;Pwd=;Allow Zero Datetime=True;");
            for (int i = 0; i < connection.Length; i++)
                result += ", " + connection[i].ToString();
            result = result.Remove(result.IndexOf(','), 1);
            result += " };";
            MessageBox.Show(result);
            */
            //MessageBox.Show(PartyPosition("jfinmoral").ToString());
            //TP("zeusafk");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Job = "Archer"; 
            LoadControls();
            //Form2 frm2 = new Form2();
            //frm2.Show();
        }

        

        private void txtWindowsName_DropDown(object sender, EventArgs e)
        {
            txtWindowsName.Items.Clear();
            System.Diagnostics.Process[] Proceso = System.Diagnostics.Process.GetProcessesByName("KnightOnLine");
            for (int i = 0; i < Proceso.Length; i++)
                txtWindowsName.Items.Add(Proceso[i].MainWindowTitle);
        }

        public string[,] TSkills = new string[20, 3];

        private void chkAutoLoot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoLoot.Checked)
                WriteMemory(new IntPtr(ToInt32(ReadMemory(new IntPtr(PTR_DLG))) + 0x7BC), 1);
            else
                WriteMemory(new IntPtr(ToInt32(ReadMemory(new IntPtr(PTR_DLG))) + 0x7BC), 0); // 7BC
        }

        public void TimedSkill(string Skill)
        {
            switch (Skill)
            {
                case "Wolf": Wolf(); break;
                case "Swift": Swift(); break;
                case "Light Feet": LightFeet(); break;
                case "Evade": Evade(); break;
                case "Safely": Safely(); break;
                case "Skaled Skin": SkaledSkin(); break;
                case "Lupin Eyes": LupinEyes(); break;
                case "Hide": RogueHide(); break;

                case "Gain": Gain(); break;
                case "Defense": Defense(); break;
                case "Sprint": Sprint(); break;

                case "Strenght": Strenght(CharID()); break;
                case "Prayer Of Cronos": PlayerOfCronos(); break;
                case "Prayer Of God's Power": PlayerOfGodPower(); break;
                case "Blasting": Blasting(); break;
                case "Wildness": Wildness(); break;
                default: return;
            }
        }

        private void TimerTSkills_Tick(object sender, EventArgs e)
        {

            if (CharDC() == 0)
                System.Media.SystemSounds.Exclamation.Play();

            int j = -1; int k = -1;
            switch (CharJob())
            {
                case "Rogue": j = 0; k = 8; break;
                case "Warrior": j = 8; k = 3; break;
                case "Priest": j = 11; k = 5; break;
                case "Mage": j = 0; k = 1; break;
                default: return;
            }
            for (int i = 0; i < k; i++)
                if (lstTimedSkills.GetItemChecked(i))
                    if (int.Parse(TSkills[i + j, 2]) == int.Parse(TSkills[i + j, 1]))
                    {
                        TSkills[i + j, 2] = "0"; TimedSkill(TSkills[i + j, 0]);
                    }
                    else
                        TSkills[i + j, 2] = (int.Parse(TSkills[i + j, 2]) + 1).ToString();
                else
                    TSkills[i + j, 2] = TSkills[i + j, 1];

            if (TransformStatus != -1)
            {
                if (TransformStatus == 0)
                    Transform(cmbTransformations.Text);
                else
                    TransformStatus--;
            }

            if (chkFollowParty.Checked && !cmbPartyNames.Text.Equals("") && PartyInRange(PartyPosition(cmbPartyNames.Text)) && CharX() != PartyX(PartyPosition(cmbPartyNames.Text)) && CharY() != PartyY(PartyPosition(cmbPartyNames.Text)))
            {
                GoTo(PartyX(PartyPosition(cmbPartyNames.Text)), PartyY(PartyPosition(cmbPartyNames.Text)));
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xa1, 0x2, 0);
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.close_hover;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.close;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.minimize_hover;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.minimize;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.minimize;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = ZeusAFK_koxp.NET.Properties.Resources.close;
        }

        //private void timer_query_Tick(object sender, EventArgs e)
        //{
        //    /*
        //    System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("koxp?x=123443211");
        //    myRequest.Method = "GET";
        //    System.Net.WebResponse myResponse = myRequest.GetResponse();
        //    System.IO.StreamReader sr = new System.IO.StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
        //    string result = sr.ReadToEnd();
        //    sr.Close();
        //    myResponse.Close();
        //    */
        //    timer_query.Enabled = false;
        //    string user = secure_string(txtIdentifier.Text.Substring(0, 4));
        //    string password = secure_string(txtIdentifier.Text.Substring(4, 4));
        //    String sql = "SELECT commands.command, commands.verification " +
        //                "FROM commands  " +
        //                "    INNER JOIN accounts " +
        //                "      ON commands.id = accounts.id " +
        //                "WHERE accounts.user = '" + user + "' and accounts.password = '" + password + "' and commands.channel = '" + ch1.Text + "'";
        //    ArrayList result = mysql_queryReturn(sql, 2);

        //    if (Last.Equals(""))
        //    {
        //        Last = (String)result[1];
        //        timer_query.Enabled = true;
        //        return;
        //    }

        //    string command = (String)result[0];
        //    New = (String)result[1];

        //    if (!New.Equals("") && New != Last)
        //    {
        //        Last = New;
        //        EjecutarComandos(command);
        //        //MessageBox.Show(command);
        //    }

        //    timer_query.Enabled = true;
        //}

        private void btnConnectRemote_Click(object sender, EventArgs e)
        {
            if (timer_query.Enabled == true)
            {
                timer_query.Enabled = false;
                Last = "";
                btnConnectRemote.Text = "Conectar";
            }
            else
            {
                timer_query.Enabled = true;
                btnConnectRemote.Text = "Desconectar";
            }
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            if (btnTransform.Text.Equals("Activar"))
            {
                TransformStatus = 0;
                btnTransform.Text = "Desactivar";
            }
            else
            {
                TransformStatus = -1;
                btnTransform.Text = "Activar";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (writer != null)
                    writer.Close();
            }
            catch { }
            //if (conn.State != ConnectionState.Closed)
            //    conn.Close();
        }

        private void ch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Last = "";
        }

        private void timerPriest_Tick(object sender, EventArgs e)
        {
            if (chkHeals.Checked && !txtHeals.Text.Equals("") && !cmbHeals.Text.Equals(""))
                if (chkLowerLife.Checked)
                {
                    int LowerLife = 101;
                    int Member = -1;
                    for (int i = 1; i <= PartyMembers(); i++)
                        if ((PartyHP(i) * 100) / PartyMaxHP(i) < LowerLife && PartyInRange(i))
                        {
                            LowerLife = (PartyHP(i) * 100) / PartyMaxHP(i);
                            Member = i;
                        }
                    if (PartyHP(Member) < ((PartyMaxHP(Member) * int.Parse(txtHeals.Text)) / 100))
                        Heal(int.Parse(HealChooser(cmbHeals.Text, Member)).ToString(), PartyID(Member));
                }
                else
                {
                    for (int i = 1; i <= PartyMembers(); i++)
                        if (PartyHP(i) < ((PartyMaxHP(i) * int.Parse(txtHeals.Text)) / 100) && PartyInRange(i))
                        {
                            Heal(int.Parse(HealChooser(cmbHeals.Text, i)).ToString(), PartyID(i));
                            break;
                        }
                }

            if (chkMyHeal.Checked && !txtMyHeal.Text.Equals("") && !cmbMyHeal.Text.Equals(""))
                if (CharHP() < (CharMaxHP() * int.Parse(txtMyHeal.Text)) / 100)
                    Heal(HealChooser(cmbMyHeal.Text), CharID());

            if (chkMyBuff.Checked && !cmbMyBuff.Text.Equals(""))
            {
                if (CharMaxHP() != MyLife)
                {
                    Buff(cmbMyBuff.Text, CharID());
                    MyLife = CharMaxHP();
                    if (chkMyAC.Checked && !cmbMyAC.Text.Equals(""))
                        AC(cmbMyAC.Text, CharID());
                }
            }
            else MyLife = 0;

            if (chkBuffs.Checked && !cmbBuffs.Text.Equals(""))
            {
                for (int i = 1; i <= PartyMembers(); i++)
                    if (PartyMaxHP(i) != PartyLifes[i - 1])
                    {
                        Buff(cmbBuffs.Text, PartyID(i));
                        if (chkACs.Checked && !cmbACs.Text.Equals(""))
                            AC(cmbACs.Text, PartyID(i));
                        PartyLifes[i - 1] = PartyMaxHP(i);
                        break;
                    }
            }
            else
                PartyLifes = new int[8];

            if (chkMalice.Checked && !EnemyID().Equals(LastMaliced) && !EnemyID().Equals("FFFF"))
            {
                Malice(EnemyID());
                LastMaliced = EnemyID();
            }
        }

        private void cmbPartyNames_DropDown(object sender, EventArgs e)
        {
            cmbPartyNames.Items.Clear();
            for (int i = 1; i <= PartyMembers(); i++)
                cmbPartyNames.Items.Add(PartyName(i));
        }

        private void btnSkillUp_Click(object sender, EventArgs e)
        {
            if (lstSkills.SelectedIndex > 0)
            {
                string tmpSkill = lstSkills.Items[lstSkills.SelectedIndex - 1].ToString();
                lstSkills.Items[lstSkills.SelectedIndex - 1] = lstSkills.SelectedItem.ToString();
                lstSkills.Items[lstSkills.SelectedIndex] = tmpSkill;
                lstSkills.SelectedIndex = lstSkills.SelectedIndex - 1;
            }
        }

        private void btnSkillDown_Click(object sender, EventArgs e)
        {
            if (lstSkills.SelectedIndex < lstSkills.Items.Count - 1)
            {
                string tmpSkill = lstSkills.Items[lstSkills.SelectedIndex + 1].ToString();
                lstSkills.Items[lstSkills.SelectedIndex + 1] = lstSkills.SelectedItem.ToString();
                lstSkills.Items[lstSkills.SelectedIndex] = tmpSkill;
                lstSkills.SelectedIndex = lstSkills.SelectedIndex + 1;
            }
        }

        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            frm_AddSkill AddSkill = new frm_AddSkill();
            AddSkill.SkillList = cSkills;
            AddSkill.ShowDialog();
            ArrayList result = AddSkill.SelectedSkills;
            for (int i = 0; i < result.Count; i++)
                if (!lstSkills.Items.Contains(result[i]))
                    lstSkills.Items.Add(result[i]);
        }

        private void btnRemoveSkill_Click(object sender, EventArgs e)
        {
            try { lstSkills.Items.RemoveAt(lstSkills.SelectedIndex); }
            catch { }
        }
        
        void BtnPartyAddClick(object sender, EventArgs e)
        {
        	if (!txtInviteName.Text.Equals(""))
                InviteParty(txtInviteName.Text);
            else
                if (!EnemyName().Equals("") && FormatDec(EnemyID(), 4) <= 9999)
                    InviteParty(EnemyName());
        }

        private void imgDonate_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        public void ApplyLanguaje(string file)
        {
            foreach (Control c in Controls)
                UpdateControl(c, file);
        }

        public void UpdateControl(Control c, string file)
        {
            if (c is GroupBox)
                foreach (Control c1 in c.Controls)
                    UpdateControl(c1, file);
            else if (c is TabControl)
                foreach (Control c1 in c.Controls)
                    UpdateControl(c1, file);
            else if (c is TabPage)
            {
                c.Text = ReadINI(file, "Controls", c.Name);
                foreach (Control c1 in c.Controls)
                    UpdateControl(c1, file);
            }
            else if (!c.Text.Equals(""))
                c.Text = ReadINI(file, "Controls", c.Name);
        }

        public string SelectLanguaje()
        {
            Form frm = new Form();
            Label lbl = new Label();
            ComboBox cmb = new ComboBox();
            Button btnOK = new Button();
            Button btnCancel = new Button();

            lbl.Text = "Select an languaje:";
            lbl.Location = new Point(10, 10);
            lbl.AutoSize = true;

            cmb.Size = new Size(250, 15);
            cmb.Location = new Point(10, 30);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            ArrayList files = LoadLanguajes();
            if (files.Count == 0)
            {
                MessageBox.Show("No languaje file was found in the current directory, you can download it from the forum.", "Languaje file not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return string.Empty;
            }
            foreach (string[] file in files)
                cmb.Items.Add(file[0]);

            btnOK.Text = "Select";
            btnOK.Location = new Point(100, 60);
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(185, 60);
            btnCancel.DialogResult = DialogResult.Cancel;

            frm.Text = "Change languaje...";
            frm.Size = new Size(280, 120);
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.AcceptButton = btnOK;
            frm.CancelButton = btnCancel;

            frm.Controls.AddRange(new Control[] { lbl, cmb, btnOK, btnCancel });

            while (frm.ShowDialog() == DialogResult.OK)
            {
                if (!cmb.Text.Trim().Equals("") && cmb.SelectedIndex != -1)
                    return ((string[])files[cmb.SelectedIndex])[1];
            }
            return string.Empty;
        }

        public ArrayList LoadLanguajes()
        {
            ArrayList result = new ArrayList();
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\lang")) Directory.CreateDirectory(Application.StartupPath + "\\lang");
            }
            catch { MessageBox.Show("An error has ocurred when try to create lang folder, create it manually and try again.", "Error creating lang folder", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
            string[] files = Directory.GetFiles(Application.StartupPath + "\\lang", "*.ini");
            foreach (string file in files)
            {
                try
                {
                    string title = "";
                    if (!(title = ReadINI(file, "Languaje", "Name")).Equals(""))
                    {
                        result.Add(new string[] { title, file });
                    }
                }
                catch { }
            }
            return result;
        }

        public void AnalizeControl(Control c)
        {
            if (c is GroupBox)
                foreach (Control c1 in c.Controls)
                    AnalizeControl(c1);
            else if (c is TabControl)
                foreach (Control c1 in c.Controls)
                    AnalizeControl(c1);
            else if (c is TabPage)
                foreach (Control c1 in c.Controls)
                    AnalizeControl(c1);
            else if (!c.Text.Equals(""))
                WriteINI("", "Controls", c.Name, c.Text);
        }

        private void btnLanguaje_Click(object sender, EventArgs e)
        {
            string file = SelectLanguaje();
            if (!file.Equals(string.Empty))
                ApplyLanguaje(file);
        }
    }
}
