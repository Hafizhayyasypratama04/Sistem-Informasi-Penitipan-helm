using System;
using System.Collections.Generic;

namespace PenitipanHelm
{
    class Program
    {
        static List<Tuple<int, string, string, string, DateTime, bool>> listHelm = new List<Tuple<int, string, string, string, DateTime, bool>>();
        static int nomorHelmTerakhir = 1;

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\tPenitipan HELM Politeknik STMI Jakarta\n");
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Menitipkan Helm");
                Console.WriteLine("2. Daftar Helm");
                Console.WriteLine("3. Mengambil Helm");
                Console.Write("Pilih Menu (1-3): ");
                string inputMenu = Console.ReadLine();

                switch (inputMenu)
                {
                    case "1":
                        Console.WriteLine("\nMasukkan informasi penitipan helm:");
                        Console.Write("Masukkan nama pengguna: ");
                        string nama = Console.ReadLine();

                        Console.Write("Masukkan merk helm: ");
                        string merk = Console.ReadLine();

                        Console.Write("Masukkan warna helm: ");
                        string warna = Console.ReadLine();

                        DateTime tanggal;
                        bool inputTanggalValid = false;
                        do
                        {
                            Console.Write("Masukkan tanggal (dd/MM/yyyy): ");
                            inputTanggalValid = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tanggal);

                            if (!inputTanggalValid)
                            {
                                Console.WriteLine("Format tanggal salah. Gunakan format yang benar (misal: 01/01/2023)");
                            }

                        } while (!inputTanggalValid);

                        TimeSpan jam;
                        bool inputJamValid = false;
                        do
                        {
                            Console.Write("Masukkan jam penitipan (HH:mm): ");
                            string inputJam = Console.ReadLine();

                            if (TimeSpan.TryParseExact(inputJam, "HH:mm", null, System.Globalization.TimeSpanStyles.None, out jam))
                            {
                                inputJamValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Format jam salah. Gunakan format yang benar (misal: 09:00)");
                            }
                        } while (!inputJamValid);

                        DateTime jamAwal = tanggal.Date.Add(jam);
                        listHelm.Add(new Tuple<int, string, string, string, DateTime, bool>(nomorHelmTerakhir, nama, merk, warna, jamAwal, false));
                        Console.WriteLine($"Nomor helm Anda: {nomorHelmTerakhir}");
                        nomorHelmTerakhir++;
                        Console.WriteLine("Biaya penitipan helm = Rp. 500/Jam\n ");
                        Console.WriteLine("Penitipan helm berhasil ditambahkan.\n");
                        break;

                    case "2":
                        Console.WriteLine("\nDaftar Helm yang Ada:");
                        if (listHelm.Count == 0)
                        {
                            Console.WriteLine("Belum ada helm yang terdaftar.\n");
                            break;
                        }

                        foreach (var helm in listHelm)
                        {
                            Console.WriteLine($"Nomor Helm: {helm.Item1} | Nama Pengguna: {helm.Item2} | Merk: {helm.Item3} | Warna: {helm.Item4} | Tanggal: {helm.Item5.ToString("dd/MM/yyyy HH:mm")} | Status Diambil: {(helm.Item6 ? "Sudah diambil" : "Belum diambil")}");
                        }
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.WriteLine("\nMasukkan informasi pengambilan helm:");
                        Console.Write("Masukkan nomor helm yang akan diambil: ");
                        int nomorHelm;
                        if (!int.TryParse(Console.ReadLine(), out nomorHelm))
                        {
                            Console.WriteLine("Input tidak valid.");
                            break;
                        }

                        Console.Write("Masukkan nama pengguna helm yang akan diambil: ");
                        string namaPengguna = Console.ReadLine();
                        Console.Write("Masukkan merk helm yang akan diambil: ");
                        string merkHelm = Console.ReadLine();
                        Console.Write("Masukkan warna helm yang akan diambil: ");
                        string warnaHelm = Console.ReadLine();

                        DateTime tanggalAmbil;
                        bool inputTanggalAmbilValid = false;
                        do
                        {
                            Console.Write("Masukkan tanggal (dd/MM/yyyy): ");
                            inputTanggalAmbilValid = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out tanggalAmbil);

                            if (!inputTanggalAmbilValid)
                            {
                                Console.WriteLine("Format tanggal salah. Gunakan format yang benar (misal: 01/01/2023)");
                            }

                        } while (!inputTanggalAmbilValid);

                        TTimeSpan jam;
                        bool inputJamValid = false;
                        do
                        {
                            Console.Write("Masukkan jam penitipan (HH:mm): ");
                            string inputJam = Console.ReadLine();

                            if (TimeSpan.TryParseExact(inputJam, "HH:mm", null, System.Globalization.TimeSpanStyles.None, out jam))
                            {
                                inputJamValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Format jam salah. Gunakan format yang benar (misal: 09:00)");
                            }
                        } while (!inputJamValid);


                        DateTime waktuAmbil = tanggalAmbil.Date.Add(jamAmbil);

                        bool helmDitemukan = false;
                        for (int i = 0; i < listHelm.Count; i++)
                        {
                            if (listHelm[i].Item1 == nomorHelm)
                            {
                                if (waktuAmbil < listHelm[i].Item5)
                                {
                                    Console.WriteLine("Harap masukkan Waktu setelah Anda menitipkan helm.");
                                    helmDitemukan = true;
                                    break;
                                }

                                TimeSpan selisihJam = waktuAmbil - listHelm[i].Item5;
                                int jamPenitipan = (int)Math.Ceiling(selisihJam.TotalHours);

                                if (jamPenitipan == 0)
                                {
                                    Console.WriteLine("Anda tidak dapat mengambil helm pada hari yang sama saat Anda menitipkannya.");
                                    helmDitemukan = true;
                                    break;
                                }

                                int biaya = jamPenitipan * 500;

                                Console.WriteLine($"Biaya penitipan helm: Rp {biaya}\n");
                                helmDitemukan = true;
                                listHelm[i] = new Tuple<int, string, string, string, DateTime, bool>(listHelm[i].Item1, listHelm[i].Item2, listHelm[i].Item3, listHelm[i].Item4, listHelm[i].Item5, true);
                                break;
                            }
                        }

                        if (!helmDitemukan)
                        {
                            Console.WriteLine("Helm tidak ditemukan.\n");
                        }
                        break;

                    case "999":
                        Console.WriteLine("Terima kasih! Program selesai.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan pilih antara 1-3.");
                        break;
                }
            }
        }
    }
}
