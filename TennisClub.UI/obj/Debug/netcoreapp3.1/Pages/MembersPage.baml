   M S B A M L   `   `   `  ????O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      H DTennisClub.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TennisClub
TennisClub 5         TennisClub.MembersPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      *localclr-namespace:TennisClub     5      -   5          N? Title$  MemberListPage??5      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??210 ?6   6-   ?? 5      6!    ? RowDefinitions 5   
   ?? 5      6   ?? 5      $
??Auto ?6   6,   ?? 5      $
??Auto ?6   6,   ?? 5      $
??Auto ?6   6,   .+?5   
   ?? 
???   ?$??0,0,1,0q?6%   6A   ? 5   
   $??1??6   $??1??6   .+?5      ?? -   5         ??Name$ ButtonRolesPage??5       ? Cursor$
 Hand??6L   $??35??6Z   $??5q?6f   $??
10,0,10,20q?6r   
???????$??Right=?6?   $	??Top=?6?   .??5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $??../Images/home.png??6   ?? 5      $	??5,0q?6    $??16(?6-   $??Light"?6;   Home6T   5      5      ?? 5      -   5        $ refresh??5      $
 Hand??6<   $??35??6J   $??5q?6V   $??	0,0,110,0q?6b   
???????$??Right=?6?   $	??Top=?6?   .??5       ?? $ 
Horizontal=?6   .+?5!      ?? $??../Images/reload.png??6   ?? 5"      $	??5,0q?6    $??16(?6-   $??Light"?6;   Herlaad pagina6^   5#      5$      5%      ?? 5&   
   $??1??6   $??0??6   
???   ?$??0,0,1,0q?6B   6^   ? 5'   
   $??1??6   $??0??6    5(      ?? 5)      6"   ?? 5*      6"   ?? 5+      6"   ?? 5,      6"   ?? 5-      6"   ?? 5.      6"   ?? 5/      6"   ?? 50      6"   ?? 51      6"   ?? 52      6"   ?? 53      6"   ?? 54      6"   .+?56      ?? $??0??6   $??Lid toevoegen/updaten??6!   $??11(?6A   $
??Bold"?6O   $??2q?6a   ?? 57      -   5         ??Name$ LabelFederatieNr??57   V   $??1??6   $
??Left=?6!   $??Federatienr.:* ??6<   $??2q?6p   ?? 58      -   5         ??Name$ TextBoxFederatieNr??58   W   $??Right=?6   $??1??62   $	??100??6?   $??25??6K   $??5q?6s   ?? 59      -   5        $ LabelVoornaam??59   R   $??2??6   $
??Left=?6!   $??Voornaam:* ??6<   $??2q?6i   ?? 5:      -   5        $ TextBoxVoornaam??5:   W   $??Right=?6   $??2??62   $	??100??6?   $??25??6K   $??5q?6p   ?? 5;      -   5        $ LabelAchternaam??5;   T   $??3??6   $
??Left=?6!   $??Achternaam:* ??6<   $??2q?6m   ?? 5<      -	   5        $ TextBoxAchternaam??5<   W   $??Right=?6   $??3??62   $	??100??6?   $??25??6K   $??5q?6r   ?? 5=      -
   5        $ LabelGeslacht??5=   R   $??4??6   $
??Left=?6!   $??Geslacht:* ??6<   $??2q?6i   ?? 5>      -   5         ??Name$ ComboBoxGeslacht??5>   X   $??Right=?6   $??4??63   $	??100??6@   $??25??6L   	 ?? SelectedIndex$	 0??6r   $??5q?6?   .@?5?      ?? $	??Man??6   ?? 5@      $??Vrouw??6   5A      ?? 5B      -   5        $ LabelGeboortedatum??5B   W   $??5??6   $
??Left=?6!   $??Geboortedatum:* ??6<   $??2q?6s   (  "System.Windows.Controls.DatePicker  5C      -   5        
  Name$
 DatePickerGeboortedatum??5C   h   $??Right=?6   $??5??65   $	??100??6B   $??25??6N   $
 Hand??6Z   
 ?? Now   SelectedDate# Z 6?   $??5q?6?   ?? 5D      -   5        $ 
LabelAdres??5D   O   $??6??6   $
??Left=?6!   $??Adres:* ??6<   $??2q?6c   ?? 5E      -   5        $ TextBoxAdres??5E   W   $??Right=?6   $??6??62   $	??100??6?   $??25??6K   $??5q?6m   ?? 5F      -   5        $ LabelNummer??5F   P   $??7??6   $
??Left=?6!   $??	Nummer:* ??6<   $??2q?6e   ?? 5G      -   5        $ TextBoxNummer??5G   W   $??Right=?6   $??7??62   $	??100??6?   $??25??6K   $??5q?6n   ?? 5H      -   5        $ LabelToevoeging??5H   Y   $??8??6   $
??Left=?6!   $??Extra toevoeging: ??6<   $??2q?6r   ?? 5I      -   5        $ TextBoxToevoeging??5I   W   $??Right=?6   $??8??62   $	??100??6?   $??25??6K   $??5q?6r   ?? 5J      -   5        $ LabelPostcode??5J   R   $??9??6   $
??Left=?6!   $??Postcode:* ??6<   $??2q?6i   ?? 5K      -   5        $ TextBoxPostcode??5K   W   $??Right=?6   $??9??62   $	??100??6?   $??25??6K   $??5q?6p   ?? 5L      -   5        $ 	LabelStad??5L   O   $??10??6   $
??Left=?6"   $??Stad:* ??6=   $??2q?6b   ?? 5M      -   5        $ TextBoxStad??5M   X   $??Right=?6   $??10??62   $	??100??6@   $??25??6L   $??5q?6m   ?? 5N      -   5        $ LabelTelefoonnr??5N   U   $??11??6   $
??Left=?6"   $??Telefoonnr.: ??6=   $??2q?6n   ?? 5O      -   5        $ TextBoxTelefoonnr??5O   X   $??Right=?6   $??11??62   $	??100??6@   $??25??6L   $??5q?6s   ?? 5P      -   5        $ SelectedMemberId??5P   +   $y?	Collapsed=?6   $??2q?6E   5Q      ?? 5R   
   $??0??6   $??2??6!   
???   ?$??0,0,1,0q?6B   6^   ? 5S   
   $??0??6   $??2??6    5T      ?? 5U      $
??Auto ?6#   62   ?? 5V      $
??Auto ?6#   62   ?? 5W      6%   .+?5Y      ?? -   5        $ ButtonAddMember??5Y      $??0??6.   $
 Hand??6\   $??28??6j   $??5q?6v   $??5q?6?   
???????$??Bottom=?6?   .??5Z      ?? $ 
Horizontal=?6   .+?5[      ?? $??../Images/add.png??6   ?? 5\      $	??5,0q?6    $??11(?6-   $??Light"?6;   	Toevoegen6Y   5]      5^      ?? 5_      -   5        $ ButtonUpdateMember??5_      $??1??61   $
 Hand??6b   $??28??6p   $??20,0,0,5q?6|   $??5q?6?   
???????$??Right=?6?   $??Bottom=?6?   .??5`      ?? $ 
Horizontal=?6   .+?5a      ?? $??../Images/updated.png??6   ?? 5b      $	??5,0q?6    $??11(?6-   $??Light"?6;   	Aanpassen6Y   5c      5d      5e      &   System.Windows.Controls.DataGrid  5g   
   -   5        $??
MemberList??5g      $??1??6%   $??1??65   $??10,40,10,40q?6B      HeadersVisibility$ Column=?6W      
IsReadOnly$
 True??6?    ?? AlternationCount$ 2??6?      AlternatingRowBackground
 ?????6?   ?? 5h   
   -   5        $ ButtonDeleteMember??5h      $??1??6-   $??2??6=   $
 Hand??6k   $??28??6y   $??	110,0,0,5q?6?   $??5q?6?   
???????
???  ??$
??Left=?6?   $??Bottom=?6?   .??5i      ?? $ 
Horizontal=?6   .+?5j      ?? $??../Images/remove.png??6   ?? 5k      $	??5,0q?6   $??11(?6)   $??Light"?67   	Verwijder6U   5l      5m      ?? 5n   
   $??Lid id:??6   $??1??6"   $??2??62   $??0,20,0,5q?6?   $??Bottom=?6Q   $??14(?6l   ?? 5o   
   -   5        $ TextBoxDeleteMember??5o   ?   $??2??6   $??1??6   $??Bottom=?6/   $
??Left=?6J   $??50??6e   $??25??6p   $??50,0,0,5q?6|   6?   ?? 5q   
   $??Filter:??6   $??	230,0,0,5q?6"   $??2??65   $??2??6B   $??14(?6R   $??Bottom=?6`   ?? 5r   
   -    5        $??filter??5r   ?   $??2??63   $??2??6C   $??Bottom=?6P   $??	0,0,100,5q?6k   $	??120??6~   $??25??6?   6?   5s      5t      