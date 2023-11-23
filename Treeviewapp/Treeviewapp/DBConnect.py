import sqlite3


class DBCard:
    def __init__(self):
        self.con = sqlite3.connect("libapp.db")
        self.cur = self.con.cursor()
        self.cur.execute(
            "create table if not exists Library_card "
            "(ID_card integer primary key not null, "
            "Surname text references Readers (ID_readers), "
            "Name text references Readers (ID_readers), "
            "Book_title text not null,  "
            "Data_issue text not null, "
            "Data_return text) "

        )

    def __del__(self):
        self.con.close()

    def showLibrarycard(self):
        self.cur.execute("select * from Library_card")
        rows = self.cur.fetchall()
        return rows

    def createCard(self, Surname, Name, Book_title, Data_issue, Data_return):
        self.cur.execute(
            f"insert into Library_card "
            f"values(NULL, '{Surname}', '{Name}', '{Book_title}', '{Data_issue}', '{Data_return}')"
        )
        self.con.commit()

    def updateCard(self, ID_card, Surname, Name, Book_title, Data_issue, Data_return):
        self.cur.execute("UPDATE Readers SET "
                         "Surname=?, Name=?, Book_title=?, Data_issue=?, Data_return=? "
                         "WHERE ID_card = ?",
                         (Surname, Name,
                          Book_title, Data_issue, Data_return, ID_card,))
        self.con.commit()

    def deleteCard(self, ID_card):
        self.cur.execute("DELETE FROM Library_card "
                         "WHERE ID_card=?", (ID_card,))
        self.con.commit()

    def search(self, Surname):
        self.cur.execute("SELECT * FROM Library_card "
                         "WHERE Surname=?", (Surname,))

        rows = self.cur.fetchall()
        return rows


class DBReader:
    def __init__(self):
        self.con = sqlite3.connect("libapp.db")
        self.cur = self.con.cursor()
        self.cur.execute(
            "create table if not exists Readers "
            "(ID_readers integer primary key not null, "
            "Surname text not null, "
            "Name text not null, "
            "Patronymic text, "
            "Adress text, "
            "Phone text not null) "
        )

    def __del__(self):
        self.con.close()

    def showReaders(self):
        self.cur.execute("select * from Readers")
        rows = self.cur.fetchall()
        return rows

    def createReader(self, Surname, Name, Patronymic, Adress, Phone):
        self.cur.execute(
            f"insert into Readers "
            f"values(NULL, '{Surname}', '{Name}', '{Patronymic}', '{Adress}', '{Phone}')"
        )
        self.con.commit()

    def updateReader(self, ID_readers, Surname, Name, Patronymic, Adress, Phone):
        self.cur.execute("UPDATE Readers SET "
                         "Surname=?, Name=?, Patronymic=?, Adress=?, Phone=? "
                         "WHERE ID = ?",
                         (Surname, Name,
                          Patronymic, Adress, Phone, ID_readers,))
        self.con.commit()

    def deleteReader(self, ID_readers):
        self.cur.execute("DELETE FROM Readers "
                         "WHERE ID=?", (ID_readers,))
        self.con.commit()
