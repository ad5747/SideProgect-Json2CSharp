using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Json2CSharp
{
    public partial class Form1 : Form
    {
        private List<string> classes = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassText.Text = string.Empty;
            var obj = JsonToObject(JsonText.Text);
            classes.Add(ObjectToClass(obj));
            var context = string.Empty;
            classes.ForEach(o => context = context + o + "\r\n");
            ClassText.Text = context;
        }

        private JObject JsonToObject(string str)
        {
            var obj = default(JObject);

            if (string.IsNullOrEmpty(str))
            {
                MessageBox.Show("請輸入JSON");
                return obj;
            }
            if (str.Trim().First() != '{' || str.Trim().Last() != '}')
            {
                MessageBox.Show("請輸入JSON");
                return obj;
            }
            try
            {
                return JsonConvert.DeserializeObject<JObject>(str.Replace("\r","").Replace("\n", "").Trim());
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.ToString());
                return obj;
            }
        }


        private string ObjectToClass(JObject jobj)
        {
            if (jobj == null) return string.Empty;
            if (jobj.Children().Count() <= 0) return string.Empty;

            var contact = string.Empty;
            foreach (var child in jobj.Children())
            {
                contact += (child.HasValues) ? GetPropretityStr(child) : $" public string {child.Path} {{ get; set; }}\r\n";
            }

            var classStr = $" class NewClass{classes.Count +1 } {{\r\n " + contact + "\r\n}";


            return classStr;
        }


        private string GetPropretityStr(JToken child)
        {
            var val = child.ToString().Split(':').Skip(1);

            return $" public {GetTypeStr(string.Join(":",val))} {child.Path} {{ get; set; }}\r\n";
        }

        private string GetTypeStr(string val)
        {
            val = val.Replace("\r", "").Replace("\n", "").Trim();
            var ptype = "string";
            if (val.First() == '[')
            {
                var a = JsonConvert.DeserializeObject(val);
                ptype = "IEnumerable<" + IEnumerableParse(JsonConvert.DeserializeObject<IEnumerable<object>>(val)) + ">";
            }
            else if (val.First() == '{') {
                classes.Add(ObjectToClass(JsonConvert.DeserializeObject<JObject>(val)));
                ptype = "NewClass" + classes.Count;
            }
            else if (val.First() == '"')
            {
                var d = DateTime.Now;
                if (DateTime.TryParse(val, out d)) ptype = ptype = "DateTime?";
            }
            else
            {
                if (val.ToUpper() == "TRUE" || val.ToUpper() == "FALSE") ptype = "bool?";
                else
                {
                    if (val.IndexOf('.') > -1)
                    {
                        var a = 0M;
                        if (Decimal.TryParse(val, out a)) ptype = "Decimal?";
                    }
                    else
                    {
                        var a = 0;
                        var _ = 0L;
                        if (int.TryParse(val, out a)) ptype = "int?";
                        else if (long.TryParse(val, out _)) ptype = "long?";
                    }
                }
            }
            return ptype;
        }

        private string IEnumerableParse(IEnumerable<object> objs)
        {
            var firstChars = objs.Select(o => o.ToString().First());
            var allTheSame = firstChars.Any(c => c == '{')? firstChars.All(c => c == '{') : true;
            allTheSame = allTheSame && firstChars.Any(c => c == '[') ? firstChars.All(c => c == '[') : true;
            allTheSame = allTheSame && firstChars.Any(c => c == '"') ? firstChars.All(c => c == '"') : true;
            if (!allTheSame)
            {
                return "object";
            }
            else
            {
                string ptype = null;
                foreach (var obj in objs) {
                    var tmp = GetTypeStr(obj.ToString());
                    if (ptype == null) ptype = tmp;
                    else {
                        if (ptype != tmp) {
                            if ((ptype == "long?" || ptype == "int?" || ptype == "Decimal?") && (tmp == "long?" || tmp == "int?" || tmp == "Decimal?"))
                            {
                                if (ptype == "Decimal?" || tmp == "Decimal?")
                                {
                                    ptype = "Decimal?";
                                    continue;
                                }
                                if (ptype == "long?" || tmp == "long?")
                                {
                                    ptype = "long?";
                                    continue;
                                }
                                ptype = "int?";
                            }
                            else
                            {
                                if ((ptype == "dateTime?" || ptype == "string") && (tmp == "dateTime?" || tmp == "string"))
                                {
                                    ptype = "string";
                                }
                                else
                                {
                                    ptype = "object";
                                }
                                break;
                            }
                        }
                    }
                }
                return ptype;
            }
        }
    }
}

