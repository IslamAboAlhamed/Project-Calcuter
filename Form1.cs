using SimpleCalcuter.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCalcuter
{
    public partial class Form1 : Form
    {
        bool Mode = true;
        float Result = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public string calculorText = "";
        public string currentInput = "";

        public class Node
        {
            public float Value;
            public Node Next;
        }

        public class Onode
        {
            public char Value;
            public Onode Next;
        }

        public Node Numbers = null;
        public Onode Operators = null;

        //============[Add Number To linked list]============
        public void InsertAtBeginning(float value)
        {
            Node newNode = new Node();
            newNode.Value = value;
            newNode.Next = Numbers;
            Numbers = newNode;
        }
        public float PopNumber()
        {
            if (Numbers == null)
                throw new InvalidOperationException("Stack is empty");

            float number = Numbers.Value;
            Numbers = Numbers.Next; // تحريك الرأس
            return number;
        }
        //====================================================

        //============[Insert At Operators To linked list]============
        int GetPrecedence(char op)
        {
            switch (op)
            {
                case '*':
                case '/':
                    return 2;
                case '+':
                case '-':
                    return 1;
                default:
                    return 0;
            }
        }
        public void InsertAtBeginning_Operators(char value)
        {
            Onode node = new Onode();
            node.Value = value;
            node.Next = Operators;
            Operators = node;
        }
        public char PopNumber_Operators()
        {
            if (Operators == null)
                throw new InvalidOperationException("Stack is empty");

            char operators = Operators.Value;
            Operators = Operators.Next;
            return operators;
        }
        void PushOperatorWithPrecedence(char newOp)
        {
            while (Operators != null && GetPrecedence(Operators.Value) >= GetPrecedence(newOp))
            {
                float num1 = PopNumber();
                float num2 = PopNumber();

                char op = PopNumber_Operators();

                float result = 0;
                switch (op)
                {
                    case '*': result = num2 * num1; break;
                    case '/': result = num2 / num1; break;
                    case '+': result = num2 + num1; break;
                    case '-': result = num2 - num1; break;
                }

                // Push الناتج على Numbers
                InsertAtBeginning(result);
            }

            // بعد ما تفرغ العمليات الأعلى أولوية، ضيف العملية الجديدة
            InsertAtBeginning_Operators(newOp);
        }
        //====================================================

        //=====================[Color Dark]=======================
        void ColorWhiteMode()
        {
            pictureBox1.Image = Resources.contrast2;
            panel1.BackColor = ColorTranslator.FromHtml("#eaebec");
            label3.ForeColor = Color.Black;
            num0.BackColor = ColorTranslator.FromHtml("#ffffff"); num0.ForeColor = Color.Black;
            num1.BackColor = ColorTranslator.FromHtml("#ffffff"); num1.ForeColor = Color.Black;
            num2.BackColor = ColorTranslator.FromHtml("#ffffff"); num2.ForeColor = Color.Black;
            num3.BackColor = ColorTranslator.FromHtml("#ffffff"); num3.ForeColor = Color.Black;
            num4.BackColor = ColorTranslator.FromHtml("#ffffff"); num4.ForeColor = Color.Black;
            num5.BackColor = ColorTranslator.FromHtml("#ffffff"); num5.ForeColor = Color.Black;
            num6.BackColor = ColorTranslator.FromHtml("#ffffff"); num6.ForeColor = Color.Black;
            num7.BackColor = ColorTranslator.FromHtml("#ffffff"); num7.ForeColor = Color.Black;
            num8.BackColor = ColorTranslator.FromHtml("#ffffff"); num8.ForeColor = Color.Black;
            num9.BackColor = ColorTranslator.FromHtml("#ffffff"); num9.ForeColor = Color.Black;
            button7.BackColor = ColorTranslator.FromHtml("#ffffff"); button7.ForeColor = Color.Black;
        }
        void ColorDrakMode()
        {
            pictureBox1.Image = Resources.contrast1;
            panel1.BackColor = Color.FromArgb(13, 2, 49);
            label3.ForeColor = Color.White;
            num0.BackColor = Color.FromArgb(56, 43, 98); num0.ForeColor = Color.White;
            num1.BackColor = Color.FromArgb(56, 43, 98); num1.ForeColor = Color.White;
            num2.BackColor = Color.FromArgb(56, 43, 98); num2.ForeColor = Color.White;
            num3.BackColor = Color.FromArgb(56, 43, 98); num3.ForeColor = Color.White;
            num4.BackColor = Color.FromArgb(56, 43, 98); num4.ForeColor = Color.White;
            num5.BackColor = Color.FromArgb(56, 43, 98); num5.ForeColor = Color.White;
            num6.BackColor = Color.FromArgb(56, 43, 98); num6.ForeColor = Color.White;
            num7.BackColor = Color.FromArgb(56, 43, 98); num7.ForeColor = Color.White;
            num8.BackColor = Color.FromArgb(56, 43, 98); num8.ForeColor = Color.White;
            num9.BackColor = Color.FromArgb(56, 43, 98); num9.ForeColor = Color.White;
            button7.BackColor = Color.FromArgb(56, 43, 98); button7.ForeColor = Color.White;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Mode == true)
            {
                ColorWhiteMode();
                Mode = false;
            }
            else
            {
                ColorDrakMode();
                Mode = true;
            }
        }
        //=============================================================


        //======================[Add Number]===========================
        void ListAddnumber(Button but)
        {
            if (label3.Text == "0")
            {
                label3.Text = "";
                Result = 0;
            }

            currentInput += but.Tag.ToString();
            label3.Text += but.Tag.ToString();
        }
        private void NumberAll(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ListAddnumber(btn);
        }
        //=============================================================

        //=======================[4 Buttons]===========================
        void ListCheckCalculator(Button but)
        {
            if (label3.Text == "0")
            {
                label3.Text = "";
                Result = 0;
            }

            // Add Number [list Node]
            if (currentInput != "")
            {
                InsertAtBeginning(Convert.ToSingle(currentInput));
                PushOperatorWithPrecedence(Convert.ToChar(but.Tag));
                label3.Text += but.Tag.ToString();
                currentInput = "";
            }

            if (Operators != null)
            {
                PopNumber_Operators();
                PushOperatorWithPrecedence(Convert.ToChar(but.Tag));
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);
                label3.Text += but.Tag.ToString();
                currentInput = "";
            }
            else
            {
                PushOperatorWithPrecedence(Convert.ToChar(but.Tag));
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);
                label3.Text += but.Tag.ToString();
                currentInput = "";
            }
        }
        private void ClickAllOperator(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ListCheckCalculator(btn);
        }
        //==============================================================

        private void EraseNumber(object sender, EventArgs e)
        {
            Result = 0;
            label3.Text = "0";

            while (Numbers != null)
            {
                Numbers = Numbers.Next;
            }
            while (Operators != null)
            {
                Operators = Operators.Next;
            }
        }

        private void EraseLable(object sender, EventArgs e)
        {
            if (currentInput != "")
            {
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);
                currentInput = currentInput.Substring(0,currentInput.Length - 1);
            }
            else
            {
                if (Operators != null)
                {
                    PopNumber_Operators();
                    label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);
                }
                else if(Numbers != null) {

                    currentInput = PopNumber().ToString();
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);

                    if (currentInput.Length > 0) {


                        if (currentInput != "")
                        {
                            InsertAtBeginning(Convert.ToSingle(currentInput));
                        } 
                        
                    
                    }
                }

            }

            


        }

        //===================[Result Number Calculator]=======================
        void reverse()
        {
            Onode prev = null;
            Onode current = Operators;
            Onode next = null;

            while (current != null)
            {
                next = current.Next;  // خزن التالي
                current.Next = prev;  // قلب المؤشر
                prev = current;       // حرك prev للأمام
                current = next;       // حرك current للأمام
            }

            Operators = prev; // رأس جديد بعد العكس
        }
        void calclutor_Number()
        {
            // لو في رقم لسه مكتوب
            if (currentInput != "")
            {
                InsertAtBeginning(Convert.ToSingle(currentInput));
                currentInput = "";
            }

            if (Numbers.Next != null)
            {

            

                // طول ما في عمليات
                while (Operators != null)
                {
                    float num1 = PopNumber(); // آخر رقم
                    float num2 = PopNumber(); // اللي قبله

                    char op = PopNumber_Operators();

                    float result = 0;

                    switch (op)
                    {
                        case '*': result = num2 * num1; break;
                        case '/': result = num2 / num1; break;
                        case '+': result = num2 + num1; break;
                        case '-': result = num2 - num1; break;
                    }

                    // 🔥 أهم سطر
                    InsertAtBeginning(result);
                }

                // في الآخر هيبقى رقم واحد بس
                if (Numbers != null)
                {
                    float finalResult = PopNumber();
                    label3.Text = finalResult.ToString();
                }
            }
        }
        private void ClickRusert(object sender, EventArgs e)
        {
            calclutor_Number();
        }

        //====================================================================
    }
}