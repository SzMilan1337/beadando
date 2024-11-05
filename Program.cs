using beadando;
using System.Text;

List<Versenyzo> versenyzok = [];

using StreamReader sr = new("..\\..\\..\\src\\feladat.txt", Encoding.UTF8);
while (!sr.EndOfStream) versenyzok.Add(new(sr.ReadLine()));

Console.WriteLine($"versenyzok szama: {versenyzok.Count}");

var elit = versenyzok.Count(v => v.Kategoria == "elit junior");
Console.WriteLine($"Elit junior kategoriaban a versenyzok szama: {elit} fo");

var atlag = versenyzok.Where(v=>v.Nem)
    .Average(v => 2014 - v.SzulEv);
Console.WriteLine($"A férfi versenyzok atlageletkora: {atlag:0.00} ev");

var futas = versenyzok.Sum(v => v.Idok["futás"].TotalHours);
Console.WriteLine($"futással toltott ido: {futas:0.00} ora");

var atlagosusz = versenyzok
    .Where(v => v.Kategoria == "20-24")
    .Average(v => v.Idok["úszás"].TotalMinutes);
Console.WriteLine($"Átlagos uszasi ido a(z) 20-24 kategoriaban: {atlagosusz:0.00} perc");

var noigy = versenyzok
    .Where(v => !v.Nem)
    .MinBy(v => v.OsszIdo);
Console.WriteLine($"Gyoztes nő: {noigy}");

var nemenkent = versenyzok.GroupBy(v => v.Nem).OrderBy(g => g.Key);
Console.WriteLine("Nemenként a versenyt befejezok szama:");
foreach (var grp in nemenkent)
{
    Console.WriteLine($"\t{grp.Key,11}: {grp.Count(),2}");
}

var korkatekoriankent = versenyzok
    .GroupBy(v => v.Kategoria)
    .ToDictionary(g => g.Key, g => g.Average(v =>
        v.Idok["I. depó"].TotalMinutes +
        v.Idok["II. depó"].TotalMinutes))
    .OrderBy(kvp => kvp.Key);

Console.WriteLine("Kategoriankent az atlagos depo ido:");
foreach (var kvp in korkatekoriankent)
{
    Console.WriteLine($"\t{kvp.Key,11}: {kvp.Value:0.00} perc");
}