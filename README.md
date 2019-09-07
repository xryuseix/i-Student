# Title: i Student

## 【本プログラムについて】

* 本プログラムは教師が生徒の名前と顔を一致させる為の学習ソフトです。

## 【起動方法】

* インストーラを利用し、i Stdent.exe又はデスクトップ上のショートカットで起動してください。

## 【使用方法】

* マウス/キーボードともに使用可能です。
* クイズを開始...クイズを開始します。
* 成績照会...生徒情報や正答率が確認できます。
* HOME...メインメニューに戻ります

## 【実行環境】

* Windows10 homeで実行は確認しました。

## 【ファイル構造】

写真はbin/Debug/Picturesの中にある
DBファイルはbin/Debug/StudentDBである

## 【DB概要】

以下の項目がある

| 項目名 | 型 | 使用方法 |
|-----------:|:------------:|:------------|
| Number | TEXT | 学生番号         |
| Name | TEXT | 学生氏名 |
| Class | TEXT | クラス |
| Gender | TEXT | 性別 |
| Correct | INTEGER | 正解数 |
| Answer | INTEGER | 回答数 |
| Note | TEXT | メモ |
| Pic1 | TEXT | 生徒の写真のファイル名 |
| Pic2 | TEXT | 予備 |
| Pic3 | TEXT | 担任教師名 |
