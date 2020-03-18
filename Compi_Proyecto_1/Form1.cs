using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Compi_Proyecto_1
{

    public partial class Form1 : Form
    {
        List<Set> sets;
        List<Regular_Expression> expressions;
        List<Error> errors;
        List<Error> tokensAcepted;
        List<Error> tokensNonAcepted;
        List<Token> tokens;

        public Form1()
        {
            InitializeComponent();
            openFile.Filter = "Text|*.er";
            openFile.InitialDirectory = "c:\\";
            openFile.RestoreDirectory = true;
            addTab();
            type_image.Items.Add("AFN");
            type_image.Items.Add("AFD");
            type_image.Items.Add("TRANSICIONES");
        }

        private void addTab()
        {
            TabPage tp = new TabPage("Page");
            tabControl.TabPages.Add(tp);

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.Multiline = true;
            tb.WordWrap = false;
            tb.ScrollBars = ScrollBars.Both;
            tp.Controls.Add(tb);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK) // Test result.
            {
                string file = openFile.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    if(tabControl.SelectedTab.Controls[0].Text.Equals(""))
                    {
                        tabControl.SelectedTab.Controls[0].Text = text;
                    }else
                    {
                        addTab();
                        tabControl.Controls[tabControl.TabCount-1].Controls[0].Text = text;
                        tabControl.SelectTab(tabControl.TabCount - 1);
                    }
                }
                catch (IOException) { }
            }
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage actual = tabControl.SelectedTab;
            if(actual != null)
                tabControl.TabPages.Remove(actual);
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTab();
        }

        private void loadThompsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_afn(tabControl.SelectedTab.Controls[0].Text);
        }

        private void create_afn(string content)
        {
            this.sets = new List<Set>();
            this.expressions = new List<Regular_Expression>();
            this.errors = new List<Error>();
            this.tokensAcepted = new List<Error>();
            this.tokensNonAcepted = new List<Error>();
            this.tokens = new List<Token>();

            int size_content = content.Length;
            String lexical_expression = "", pattern;
            int pointer = 0, initial_pointer = 0;
            Boolean expression = false, set = false;
            int row = 0;
            int previos_pointer_line = 0;

            while (true)
            {
                if (content.ElementAt(pointer) == ' ')
                {
                    pointer += 1;
                    continue;
                }
                else if (content.Substring(pointer, 2) == "\r\n")
                {
                    pointer += 2;
                    initial_pointer = pointer;
                    previos_pointer_line = pointer;
                    row++;
                    continue;
                }
                else if (content.Substring(pointer, 2).Equals("//"))
                {
                    while (true)
                    {
                        if (content.Substring(pointer, 2) == "\r\n")
                            break;
                        pointer += 1;
                    }
                    row++;
                    pointer += 2;
                    initial_pointer = pointer;
                    previos_pointer_line = pointer;
                    continue;
                }
                else if (content.Substring(pointer, 2).Equals("<!"))
                {
                    while (true)
                    {
                        if (content.Substring(pointer, 2).Equals("!>"))
                            break;
                        else if (content.Substring(pointer, 2) == "\r\n")
                        {
                            row++;
                            pointer += 2;
                            continue;
                        }

                        pointer++;
                    }
                    pointer += 2;
                    continue;
                }
                else if (content.Substring(pointer, 5).Equals("conj:", StringComparison.OrdinalIgnoreCase))
                {
                    pointer += 5;
                    while (content.ElementAt(pointer) == ' ')
                        pointer += 1;
                    initial_pointer = pointer;
                    set = true;
                }
                else if (content.Substring(pointer, 2).Equals("->") && expression == false)
                {
                    expression = true;
                    lexical_expression = "";

                    while (true)
                    {
                        if (pointer == initial_pointer || content.ElementAt(initial_pointer) == ' ')
                            break;

                        lexical_expression += content.ElementAt(initial_pointer);
                        initial_pointer += 1;
                    }
                    pointer += 2;
                    while (content.ElementAt(pointer) == ' ')
                        pointer += 1;
                    initial_pointer = pointer;
                    continue;
                }
                else if (expression && content.ElementAt(pointer) == ';')
                {
                    expression = false;
                    pattern = content.Substring(initial_pointer, pointer -initial_pointer);
                    initial_pointer = pointer + 1;
                    if (set == true)
                        sets.Add(new Set(pattern, lexical_expression));
                    else
                        expressions.Add(new Regular_Expression(pattern, lexical_expression));
                    set = false;
                }
                else if (!expression && content.ElementAt(pointer) == ':')
                {

                    lexical_expression = "";

                    while (true)
                    {
                        if (pointer == initial_pointer || content.ElementAt(initial_pointer) == ' ')
                            break;

                        lexical_expression += content.ElementAt(initial_pointer);
                        initial_pointer += 1;
                    }

                    char aux = content.ElementAt(++pointer);
                    while(aux != '"')
                        aux = content.ElementAt(++pointer);

                    int column = pointer - previos_pointer_line;

                    aux = content.ElementAt(++pointer);
                    string value = "";

                    while (aux != '"' || content.Substring(pointer, 2).Equals("\\\""))
                    {
                        if(!content.Substring(pointer, 2).Equals("\\\""))
                            value += aux;
                        else
                        {
                            value += "\\\"";
                            pointer++;
                        }
                        aux = content.ElementAt(++pointer);
                    }
                    aux = content.ElementAt(++pointer);
                    while (aux != ';')
                        aux = content.ElementAt(++pointer);
                    initial_pointer = ++pointer;

                    while (true)
                    {
                        if (pointer >= size_content - 2 || content.Substring(pointer, 2) == "\r\n")
                            break;
                        pointer += 1;
                    }
                    row++;
                    pointer += 2;
                    initial_pointer = pointer;

                    tokens.Add(new Token(lexical_expression, value, row, column));
                }

                pointer += 1;
                if (pointer == size_content -1 || pointer >= size_content)
                    break;
            }
            
            foreach (Regular_Expression reg in expressions)
            {
                expression_regular.Items.Add(reg.get_lexical_component());
                reg.generate_afn();
                reg.enumerate_afn();
                reg.create_afn_graph();
            }
            foreach(Set set1 in sets)
            {
                set1.analize_pattern();
            }
            
        }

        private void button_graph_Click(object sender, EventArgs e)
        {
            string expression = expression_regular.GetItemText(expression_regular.SelectedItem);
            string ruta = Application.StartupPath + "\\Images\\";

            foreach (Regular_Expression exp in expressions)
            {
                if (exp.get_lexical_component().Equals(expression))
                {
                    string type_im = type_image.GetItemText(type_image.SelectedItem);

                    if(type_im.Equals("AFN"))
                    {
                        pictureBox.Image = new Bitmap(ruta + exp.get_lexical_component() + "_afn.jpg");
                    }else if (type_im.Equals("AFD"))
                    {
                        pictureBox.Image = new Bitmap(ruta + exp.get_lexical_component() + "_afd.jpg");
                    }
                    else if (type_im.Equals("TRANSICIONES"))
                    {
                        pictureBox.Image = new Bitmap(ruta + exp.get_lexical_component() + "_transitions.jpg");
                    }
                }
            }
        }

        private void create_xml_token()
        {
            XmlTextWriter writer;
            writer = new XmlTextWriter(Application.StartupPath + "\\Reports\\" + "tokens.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();

            writer.WriteStartElement("ListaTokens");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer ss = new XmlSerializer(typeof(Token));

            foreach(Token t in tokens)
                ss.Serialize(writer, t, ns);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void create_xml_error()
        {
            XmlTextWriter writer;
            writer = new XmlTextWriter(Application.StartupPath + "\\Reports\\" + "errors.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();

            writer.WriteStartElement("ListaErrores");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer ss = new XmlSerializer(typeof(Error));

            foreach (Error t in errors)
                ss.Serialize(writer, t, ns);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void create_xml_accepted()
        {
            XmlTextWriter writer;
            writer = new XmlTextWriter(Application.StartupPath + "\\Reports\\" + "accepted.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();

            writer.WriteStartElement("Lista_Tokens_Accepted");
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer ss = new XmlSerializer(typeof(Error));

            foreach (Error t in tokensAcepted)
                ss.Serialize(writer, t, ns);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void create_pdf_table()
        {
            string txt_table = "<h1>Errores Lexicos</h1>";
            txt_table += "<table style=\"width: 100 % \">\n<tr>\n<th>Valor</th>\n<th>Fila</th>\n<th>Columna</th></tr> ";
            foreach (Error t in tokensNonAcepted)
            {
                txt_table += "<tr>\n<td>" + t.value + "</td>\n";
                txt_table += "<td>" + t.row + "</td>\n";
                txt_table += "<td>" + t.column + "</td>\n</tr>\n";
            }
            txt_table += "</table>";

            using (FileStream stream = new FileStream(Application.StartupPath + "\\Reports\\" + "table_errors.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                StringReader sr = new StringReader(txt_table);
                

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                stream.Close();
            }

        }

        private void analize_Expressions()
        {
            if (tokens == null || tokens.Count() < 1)
                return;

            foreach(Token lexeme in tokens)
            {
                foreach(Regular_Expression exp in expressions)
                {
                    string result = exp.analize_lexeme(lexeme.value, sets, errors, lexeme.name);
                    if(!result.Equals(""))
                    {
                        console.Text += "\n" + result;
                        if (result.Substring(0, 5).Equals("Error"))
                            tokensNonAcepted.Add(new Error(lexeme.name, lexeme.column, lexeme.row));
                        else
                            tokensAcepted.Add(new Error(lexeme.name, lexeme.column, lexeme.row));
                    }
                }
            }
        }

        private void saveTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_xml_token();
        }

        private void saveErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_xml_error();
            create_pdf_table();
        }

        private void analizeLexemesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            analize_Expressions();
            create_xml_accepted();
        }
    }
}
