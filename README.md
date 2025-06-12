# FileIndexerSystem

## Projekto aprašymas

Tai paskirstytas C# projektas, sudarytas iš trijų atskirų Console Application programų: `Master`, `ScannerA` ir `ScannerB`. Sistema skirta nuskaityti `.txt` failų turinį, suskaičiuoti žodžių dažnius ir perduoti šią informaciją pagrindiniam procesui per Named Pipes kanalus.

---

## Užduotys ir versijų istorija

### 1 Užduotis – Projekto struktūra
- Sukurta `FileIndexerSolution` su trimis Console App projektais: `Master`, `ScannerA`, `ScannerB`.
- Įdėtas `.gitignore`.

### 2 Užduotiws – Failų skaitymas agentuose
- `ScannerA` ir `ScannerB` skaito `.txt` failus iš `TestData` katalogo.
- Žodžiai suskaičiuojami ir paruošiami siuntimui.
- Naudotas `Dictionary<string, int>`.

### 3 Užduotis – Named Pipe ryšys su Master
- `ScannerA` prisijungia prie `Master` per `agent1`.
- `Master` gauna ir atvaizduoja žodžius.
- Įgyvendinta bazinė komunikacija naudojant `NamedPipeClientStream` ir `NamedPipeServerStream`.

### 4 užduotis – Du agentai vienu metu
- Pridėtas `ScannerB`, kuris jungiasi per `agent2`.
- `Master` paleidžia dvi gijas, kurios priima duomenis vienu metu.
- Įsitikinta, kad abu agentai gali veikti nepriklausomai.

### 5 Užuotis – CPU branduolio paskirstymas
- `ScannerA`, `ScannerB`, `Master` priskirti prie atskirų CPU branduolių naudojant `ProcessorAffinity`.

---

## Naudoti įrankiai.

- C# (.NET 8)
- Console Applications
- Named Pipes
- Thread
- ProcessorAffinity
- ConcurrentQueue / ConcurrentDictionary

---

## Paleidimas

1. Kiekvienam agentui paruošti `TestData` aplanką su `.txt` failais.
2. Pirmiausia paleisti `Master`, tada abu agentus rankiniu būdų tiesiog paleidus .exe programas.
3. Žodžiai turėtų būti priimti ir parodyti konsolėje taip pat ir suskaičiuoti.

---
Studentas: Ignas  
