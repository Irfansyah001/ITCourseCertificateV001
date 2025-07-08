using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;

namespace ITCourseCertificateV001
{
    public partial class FormPengaturanIP : Form
    {
        string configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), // mendapatkan path ke folder AppData
            "ITCourseCertificate", "server_config.txt" // menggabungkan path dengan nama file konfigurasi
        );
        public FormPengaturanIP()
        {
            InitializeComponent();
            LoadIP(); // Load IP saat form dibuka
        }

        private void LoadIP()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    txtIP.Text = File.ReadAllText(configPath).Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca file konfigurasi: " + ex.Message);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();

            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("IP tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidIPv4(ip))
            {
                MessageBox.Show("Format IP tidak valid. Gunakan format IPv4 seperti 192.168.1.1 (atau localhost).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string folderPath = Path.GetDirectoryName(configPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            try
            {
                File.WriteAllText(configPath, ip);
                Koneksi.RefreshConnectionString();
                MessageBox.Show("IP berhasil disimpan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan file konfigurasi: " + ex.Message);
            }
        }

        private bool IsValidIPv4(string ipString)
        {
            // Menambahkan validasi untuk kasus di mana user mungkin memasukkan IP:PORT atau Server\Instance
            // IPAddress.TryParse hanya akan memparsing bagian IP.
            // Kita perlu mengecek apakah ini nama server (bukan IP) atau IP dengan port/instance.

            // Pertama, coba parse sebagai IP Address murni
            if (IPAddress.TryParse(ipString, out IPAddress ip))
            {
                return ip.AddressFamily == AddressFamily.InterNetwork; // Valid IPv4 murni
            }

            // Kedua, jika bukan IP murni, cek apakah itu nama host atau kombinasi host\instance atau host:port
            // Ini adalah regex sederhana untuk nama host/instance, tidak mencakup semua kasus tapi lebih fleksibel
            // Misalnya: "localhost", "MYSERVER\SQLEXPRESS", "192.168.1.100,1433"
            // Kita akan menggunakan pola yang sama dengan Koneksi.cs, yaitu hanya memvalidasi IP.
            // Jika Anda ingin mendukung nama server seperti "MYSERVER", maka validasi ini harus diperlonggar.
            // Asumsi: Kita hanya ingin IP Address atau IP:Port (yang bagian IP-nya valid IPv4)

            // Jika IPAddress.TryParse gagal, dan stringnya bukan IP murni,
            // kita asumsikan ini mungkin nama server atau IP dengan port/instance yang masih valid untuk koneksi
            // ke SQL Server, asalkan bagian IP dasarnya valid (jika ada).

            // Untuk konsistensi dengan Koneksi.cs, kita akan fokus pada parsing bagian IP-nya saja jika ada port/instance.
            string ipPart = ipString.Split(':')[0].Split(',')[0].Trim();

            if (IPAddress.TryParse(ipPart, out ip))
            {
                return ip.AddressFamily == AddressFamily.InterNetwork;
            }

            // Jika string bukan IP murni, dan juga bukan IP dengan port/instance yang bagian IP-nya valid,
            // maka bisa jadi itu nama host biasa (misal "localhost" atau "NAMASERVER").
            // Untuk nama host, IsValidIPv4 ini tidak bisa memvalidasinya.
            // Kita perlu memvalidasi IP saja, atau perlu Dns.GetHostEntry (tapi itu akan memakan waktu).
            // Untuk saat ini, kita stick dengan validasi IP.

            // Jika tidak bisa di-parse sebagai IP murni, dan tidak valid setelah di-split,
            // dan tidak ingin mendukung nama host (misalnya "localhost"), maka return false.
            // Jika Anda ingin memvalidasi "localhost" sebagai valid, Anda perlu menambahkan pengecekan eksplisit.
            if (ipPart.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            {
                return true; // Izinkan "localhost" sebagai input IP yang valid
            }

            return false;
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close(); // Tutup form tanpa menyimpan
        }
    }
}
