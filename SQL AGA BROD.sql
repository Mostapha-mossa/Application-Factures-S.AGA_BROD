create database Agafacture
goto
USE Agafacture

create table ville(code_v varchar(50) primary key,nom_ville varchar(50),c_postal varchar(50))
create table CLIENT(code_cl varchar(50) primary key,nom_prenom_or_raison_sociale varchar(50),Type_client varchar(50),Adresse varchar(100),N_Telephone varchar(50),N_Portable varchar(50),code_v varchar(50) foreign key references ville(code_v),Site_internet varchar(50),E_mail varchar(50),ICE varchar(50),Remarques varchar(50))
create table TVA(taux_TVA double precision, TVA_A varchar(50) primary key)
create table PRODUIE(code_p varchar(50)  primary key,Description_produie varchar(100),prix_de_vente money,TVA varchar(50) foreign key references TVA(TVA_A), Remarques varchar(50),stock int)
create table FACTURE(code_f varchar(50) primary key,code_cl varchar(50) foreign key references CLIENT(code_cl),date_Facture date,date_échéance date,Réglement varchar(50) ,TOTAL_HT money,TOTAL_TVA money,TOTAL_TTC money)
create table detal_FACTURE(code_f varchar(50)foreign key references FACTURE(code_f),code_p varchar(50)foreign key references PRODUIE(code_p),qté int,primary key (code_p,code_f))

create table FACTURE_Proforma(code_f varchar(50) primary key,code_cl varchar(50) foreign key references CLIENT(code_cl),date_Facture date,date_échéance date,Réglement varchar(50) ,TOTAL_HT money,TOTAL_TVA money,TOTAL_TTC money)
create table detal_FACTUREp(code_f varchar(50)foreign key references FACTURE_Proforma(code_f),code_p varchar(50)foreign key references PRODUIE(code_p),qté int,primary key (code_p,code_f))

create table Devis(code_d varchar(50) primary key,code_cl varchar(50) foreign key references CLIENT(code_cl),date_Facture date,date_échéance date,Réglement varchar(50) ,TOTAL_HT money,TOTAL_TVA money,TOTAL_TTC money)
create table detal_Devis(code_d varchar(50)foreign key references Devis(code_d),code_p varchar(50)foreign key references PRODUIE(code_p),qté int,primary key (code_p,code_d))

select count(CODE_v) from ville where code_v='a777'
select * from FACTURE where date_Facture between  '12/31/2021' and '12/31/2022'
select * from PRODUIE 
insert into ville values ('a777',' AGADIR','800000')
insert into TVA values (0,'Exonéré')
insert into TVA values (0.07,'07%')
insert into TVA values (0.1,'10%')
insert into TVA values (0.14,'14%')
insert into TVA values (0.2,'20%')
select * from detal_FACTURE
select * from FACTURE
update ville set nom_ville='" + textBox2.Text + "',c_postal='" + textBox3.Text + "' where CODE_V='" + textBox1.Text + "'
insert into FACTURE values ('" + maskedTextBox1.Text + "','" + comboBox2.SelectedValue +"','" + dt + "','" + dt1 + "','','','')

insert into FACTURE values ('" + maskedTextBox1.Text + "','" + maskedTextBox1.Text + "','1/1/2022',' 1/1/2029','"+comboBox1.Text+"','','','')
insert into CLIENT values ('" + maskedTextBox1.Text + "','" + textBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','a777 ','" + textBox3.Text + "','" + textBox4.Text + "','" + maskedTextBox4.Text + "','" +textBox5.Text +" ')

update FACTURE set date_échéance='" + dateTimePicker1.Value + "',date_Facture='" + dateTimePicker2.Value + "',code_cl='" + comboBox2.SelectedValue + "' where code_f='" + maskedTextBox1.Text + "'
update CLIENT set nom_prenom_or_raison_sociale='" + textBox1.Text  + "',Type_client='" + comboBox2.Text + "',Adresse='" + textBox2.Text + "',N_Telephone='" + maskedTextBox2.Text + "',N_Portable='"+ maskedTextBox3.Text +"',code_v='"+ comboBox1.SelectedValue +"',Site_internet='"+ textBox3.Text +"',E_mail='"+textBox4.Text +"',ICE='"+maskedTextBox4.Text +"' ,Remarques='"+textBox5.Text +"' where code_cl='" + maskedTextBox1.Text + "'
DROP TABLE PRODUIE
update PRODUIE set Description_produie='" + textBox1.Text + "',prix_de_vente='" + comboBox2.Text + "',taux_TVA='" + textBox2.Text + "',Remarques='" + maskedTextBox2.Text + "',stock='" + maskedTextBox3.Text + "' where code_p='" + maskedTextBox1.Text + "'
insert into PRODUIE values ('P0002','" + textBox1.Text + "',5678.66,0.2,'" + textBox3.Text + "',7 )
update PRODUIE set Description_produie='FGT',prix_de_vente=7678 ,taux_TVA=0.1,Remarques='textBox3 "',stock=8 where code_p='" + maskedTextBox1.Text + "'
DELETE FROM PRODUIE  where code_p='P0003'
DELETE FROM TVA where taux_TVA= 2
select * from Devis 

--DROP TABLE detal_FACTURE
--DROP TABLE PRODUIE
--DROP TABLE TVA
select count(code_f) from detal_FACTURE where code_f='" + comboBox2.SelectedValue + "' AND code_p='"+ comboBox1.SelectedValue + "'


CREATE PROCEDURE P1 @CodeF varchar(50)
AS
begin

update FACTURE_Proforma  set TOTAL_TVA= (select sum (TVA.taux_TVA*PRODUIE.prix_de_vente*detal_FACTUREp.qté) from TVA , PRODUIE , FACTURE_Proforma , detal_FACTUREp where TVA.TVA_A = PRODUIE.TVA and PRODUIE.code_p = detal_FACTUREp.code_p and FACTURE_Proforma.code_f= detal_FACTUREp.code_f and FACTURE_Proforma.code_f=@CodeF)  where code_f=@CodeF
update FACTURE_Proforma  set TOTAL_HT= (select sum (PRODUIE.prix_de_vente*detal_FACTUREp.qté) from PRODUIE , FACTURE_Proforma , detal_FACTUREp where PRODUIE.code_p = detal_FACTUREp.code_p and FACTURE_Proforma.code_f= detal_FACTUREp.code_f and FACTURE_Proforma.code_f=@CodeF)  where code_f=@CodeF
update FACTURE_Proforma  set TOTAL_TTC= (select TOTAL_HT+TOTAL_TVA from FACTURE_Proforma  where code_f=@CodeF) where code_f=@CodeF
select distinct  CLIENT.nom_prenom_or_raison_sociale'Nom prenom Or Raison sociale',CLIENT.Type_client'Type client',CLIENT.ICE,PRODUIE.Description_produie 'Description produit',PRODUIE.prix_de_vente 'Prix de vente',PRODUIE.TVA,detal_FACTUREp.qté 'Qté',PRODUIE.prix_de_vente*detal_FACTUREp.qté 'Montant HT',TVA.taux_TVA*PRODUIE.prix_de_vente*detal_FACTUREp.qté 'Montant TVA',FACTURE_Proforma.code_f'Référance Facture', FACTURE_Proforma.date_Facture 'Date Facture',FACTURE_Proforma.Réglement,FACTURE_Proforma.TOTAL_HT'TOTAL HT ',FACTURE_Proforma.TOTAL_TVA 'TOTAL TVA',FACTURE_Proforma.TOTAL_TTC 'TOTAL TTC' from CLIENT,TVA , PRODUIE , FACTURE_Proforma , detal_FACTUREp where TVA.TVA_A = PRODUIE.TVA and CLIENT.code_cl=FACTURE_Proforma.code_cl and PRODUIE.code_p = detal_FACTUREp.code_p and FACTURE_Proforma.code_f= detal_FACTUREp.code_f and FACTURE_Proforma.code_f=@CodeF
end
exec P3'D0001'
alter  PROCEDURE P2 @CodeF varchar(50)
AS
begin
update FACTURE  set TOTAL_TVA= (select sum (TVA.taux_TVA*PRODUIE.prix_de_vente*detal_FACTURE.qté) from TVA , PRODUIE , FACTURE , detal_FACTURE where TVA.TVA_A = PRODUIE.TVA and PRODUIE.code_p = detal_FACTURE.code_p and FACTURE.code_f= detal_FACTURE.code_f and FACTURE.code_f=@CodeF)  where code_f=@CodeF
update FACTURE  set TOTAL_HT= (select sum (PRODUIE.prix_de_vente*detal_FACTURE.qté) from PRODUIE , FACTURE , detal_FACTURE where PRODUIE.code_p = detal_FACTURE.code_p and FACTURE.code_f= detal_FACTURE.code_f and FACTURE.code_f=@CodeF)  where code_f=@CodeF
update FACTURE  set TOTAL_TTC= (select TOTAL_HT+TOTAL_TVA from FACTURE  where code_f=@CodeF) where code_f=@CodeF
select distinct  CLIENT.nom_prenom_or_raison_sociale'Nom prenom Or Raison sociale',CLIENT.Type_client'Type client',CLIENT.ICE,PRODUIE.Description_produie 'Description produit',PRODUIE.prix_de_vente 'Prix de vente',PRODUIE.TVA,detal_FACTURE.qté 'Qté',PRODUIE.prix_de_vente*detal_FACTURE.qté 'Montant HT',TVA.taux_TVA*PRODUIE.prix_de_vente*detal_FACTURE.qté 'Montant TVA',FACTURE.code_f'Référance Facture', FACTURE.date_Facture 'Date Facture',FACTURE.Réglement,FACTURE.TOTAL_HT'TOTAL HT ',FACTURE.TOTAL_TVA 'TOTAL TVA',FACTURE.TOTAL_TTC 'TOTAL TTC' from CLIENT,TVA , PRODUIE , FACTURE , detal_FACTURE where TVA.TVA_A = PRODUIE.TVA and CLIENT.code_cl=FACTURE.code_cl and PRODUIE.code_p = detal_FACTURE.code_p and FACTURE.code_f= detal_FACTURE.code_f and FACTURE.code_f=@CodeF
end
CREATE PROCEDURE P3 @CodeF varchar(50)
AS
begin

update Devis  set TOTAL_TVA= (select sum (TVA.taux_TVA*PRODUIE.prix_de_vente*detal_Devis.qté) from TVA , PRODUIE , Devis , detal_Devis where TVA.TVA_A = PRODUIE.TVA and PRODUIE.code_p = detal_Devis.code_p and Devis.code_d= detal_Devis.code_d and Devis.code_d=@CodeF)  where code_d=@CodeF
update Devis  set TOTAL_HT= (select sum (PRODUIE.prix_de_vente*detal_Devis.qté) from PRODUIE , Devis , detal_Devis where PRODUIE.code_p = detal_Devis.code_p and Devis.code_d= detal_Devis.code_d and Devis.code_d=@CodeF)  where code_d=@CodeF
update Devis  set TOTAL_TTC= (select TOTAL_HT+TOTAL_TVA from Devis  where code_d=@CodeF) where code_d=@CodeF
select distinct  CLIENT.nom_prenom_or_raison_sociale'Nom prenom Or Raison sociale',CLIENT.Type_client'Type client',CLIENT.ICE,PRODUIE.Description_produie 'Description produit',PRODUIE.prix_de_vente 'Prix de vente',PRODUIE.TVA,detal_Devis.qté 'Qté',PRODUIE.prix_de_vente*detal_Devis.qté 'Montant HT',TVA.taux_TVA*PRODUIE.prix_de_vente*detal_Devis.qté 'Montant TVA',Devis.code_D'code_d', Devis.date_Facture 'Date Facture',Devis.Réglement,Devis.TOTAL_HT'TOTAL HT ',Devis.TOTAL_TVA 'TOTAL TVA',Devis.TOTAL_TTC 'TOTAL TTC' from CLIENT,TVA , PRODUIE , Devis , detal_Devis where TVA.TVA_A = PRODUIE.TVA and CLIENT.code_cl=Devis.code_cl and PRODUIE.code_p = detal_Devis.code_p and Devis.code_d= detal_Devis.code_d and Devis.code_d=@CodeF
end
select * from TVA , PRODUIE , FACTURE , detal_FACTURE where TVA.TVA_A = PRODUIE.TVA and PRODUIE.code_p = detal_FACTURE.code_p and FACTURE.code_f= detal_FACTURE.code_f and FACTURE.code_f='F0001'

select TOTAL_HT+TOTAL_TVA from FACTURE  where code_f='F0001'

