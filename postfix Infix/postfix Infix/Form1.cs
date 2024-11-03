using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace postfix_Infix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int comparsion(char a)
        {
            if (a == '-' || a == '+')
                return 1;
            else if (a == '*' || a == '/')
                return 2;
            else if (a == '^')
                return 3;
            else
                return -1;
        }
         bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/'||c=='^'||c=='_';
        }

         bool IsInfix(string expression)
        {
            int a = 0;
            bool exp = true;
            Stack<char>stack = new Stack<char>();
            foreach (char c in expression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    a++;
                    exp = false;
                }
                else if (IsOperator(c))
                {
                    a--;
                    exp = true;
                }
                else if (c == '(')
                    stack.Push(c);
                else if (c == ')')
                {
                    if (stack.Count == 0)
                    { 
                        return false;
                    }
                    stack.Pop();
                }
                else
                return false;
                
            }
            return a == 1&& stack.Count == 0&& !exp;
           
        }
        bool isPostfix(string expression)
        {
            int a=0;
            foreach (char c in expression)
            {
               
                if(char.IsLetterOrDigit(c))
                    a++;
                else if (IsOperator(c))
                {
                    if (a < 2)
                    {
                        return false; 
                    }
                    a--;
                }
                else
                    return false;
                
            }
            return a == 1;
        }
            private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("عبارت را وارد کنید");
                }
                else
                {
                    string infix = textBox1.Text.ToString();
                    bool isInfix = IsInfix(infix);
                    if (isInfix == false)

                       MessageBox.Show("عبارت وارد شده میان وند نمیباشد");
                    else
                    {

                        Stack<char> stack = new Stack<char>();
                        foreach (char i in infix)
                        {
                            if (char.IsLetterOrDigit(i))
                                textBox2.Text += i.ToString();
                            else if (i == '(')
                                stack.Push(i);
                            else if (i == ')')
                            {
                                while (stack.Count > 0 && stack.Peek() != '(')
                                {
                                    textBox2.Text += stack.Pop().ToString();
                                }
                                stack.Pop();
                            }
                            else
                            {
                                while (stack.Count > 0 && comparsion(i) <= comparsion(stack.Peek()))
                                {
                                    textBox2.Text += stack.Pop().ToString();
                                }
                                stack.Push(i);
                            }
                        }
                        while (stack.Count > 0)
                        {
                            textBox2.Text += stack.Pop().ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("عبارت را وارد کنید");
                }
                else
                {
                    string postFix = textBox1.Text.ToString();
                    bool ispost = isPostfix(postFix);
                    if (ispost == false)
                    {
                        MessageBox.Show("عبارت وارد شده پسوندی نمیباشد");
                    }
                    else
                    {
                        Stack<string> stack = new Stack<string>();
                        foreach (char i in postFix)
                        {
                            if (char.IsLetterOrDigit(i))
                            {
                                stack.Push(i.ToString());
                            }
                            else
                            {
                                string infix1 = stack.Pop().ToString();
                                string infix2 = stack.Pop().ToString();
                                stack.Push(infix2 + i + infix1);
                            }
                        }
                        textBox2.Text = stack.Pop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox2.Enabled = false;
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text =textBox2.Text="";
                button2.Hide();
                button1.Show();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = textBox2.Text = "";
                button1.Hide();
                button2.Show();
            }
        }

       
        }
    }
    

