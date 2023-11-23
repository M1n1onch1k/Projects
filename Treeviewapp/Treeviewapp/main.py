import tkinter
from tkinter import *
from tkinter import ttk
from DBConnect import DBCard


tab_cards = DBCard()


def update():
    for elem in tv_cards.get_children(""):
        tv_cards.delete(elem)

    for rep in tab_cards.showLibrarycard():
        tv_cards.insert("", index=END, values=rep)


def addCard():
    surname = str(card_surname.get())
    name = str(card_name.get())
    book_title = str(card_title.get())
    data_issue = str(card_issue.get())
    data_return = str(card_return.get())
    tab_cards.createCard(surname, name, book_title, data_issue, data_return)
    update()


def delCard():
    for selected_item in tv_cards.selection():
        item = tv_cards.item(selected_item)
        rep = item["values"]
        tab_cards.deleteCard(int(rep[0]))
        update()


def treeitemSelect():
    item = tv_cards.item(tv_cards.selection()[0])
    rep = item["values"]
    card_surname.delete(0, END)
    card_name.delete(0, END)
    card_title.delete(0, END)
    card_issue.delete(0, END)
    card_return.delete(0, END)
    card_surname.insert(END, rep[1])
    card_name.insert(END, rep[2])
    card_title.insert(END, rep[3])
    card_issue.insert(END, rep[4])
    card_return.insert(END, rep[5])


page = Tk()
page.title("Читательский список")
page.geometry("800x500")

# Главный контейнер
frame_main = ttk.Frame(page, padding=5)

# Верхний контейнер
frame_top = ttk.Frame(frame_main, padding=5)

# Левый контейнер (там где поля)
frame_left = ttk.Frame(frame_top, padding=5)

frame_surname = ttk.Frame(frame_left, padding=5)
ttk.Label(frame_surname, text="Фамилия читателя", justify=CENTER).pack()
card_surname = ttk.Entry(frame_surname, width=30)
card_surname.pack(anchor=CENTER)
frame_surname.pack(anchor=CENTER)

frame_name = ttk.Frame(frame_left, padding=5)
ttk.Label(frame_name, text="Имя читателя", justify=CENTER).pack()
card_name = ttk.Entry(frame_name, width=30)
card_name.pack(anchor=CENTER)
frame_name.pack(anchor=CENTER)

frame_title = ttk.Frame(frame_left, padding=5)
ttk.Label(frame_title, text="Название книги", justify=CENTER).pack()
card_title = ttk.Entry(frame_title, width=30)
card_title.pack(anchor=CENTER)
frame_title.pack(anchor=CENTER)
frame_left.pack(side=LEFT, anchor=N)

frame_issue = ttk.Frame(frame_left, padding=5)
ttk.Label(frame_issue, text="Дата выдачи", justify=CENTER).pack()
card_issue = ttk.Entry(frame_issue, width=30)
card_issue.pack(anchor=CENTER)
frame_issue.pack(anchor=CENTER)
frame_left.pack(side=LEFT, anchor=N)

frame_return = ttk.Frame(frame_left, padding=5)
ttk.Label(frame_return, text="Дата получения", justify=CENTER).pack()
card_return = ttk.Entry(frame_return, width=30)
card_return.pack(anchor=CENTER)
frame_return.pack(anchor=CENTER)
frame_left.pack(side=LEFT, anchor=N)

frame_tree = ttk.Frame(frame_top, padding=5)
columns = ('ID_card','Surname', 'Name', 'Book_title', 'Data_issue', 'Data_return')
display_col = ('Surname', 'Name', 'Book_title', 'Data_issue', 'Data_return')
tv_cards = ttk.Treeview(frame_tree,columns=columns, displaycolumns=display_col, show="headings")
tv_cards.pack()
tv_cards.heading('Surname', text='Фамилия читателя')
tv_cards.heading('Name', text='Имя читателя')
tv_cards.heading('Book_title', text='Название книги')
tv_cards.heading('Data_issue', text='Дата получения книги')
tv_cards.heading('Data_return', text='Дата возвращения книги')


scrollbar = ttk.Scrollbar(frame_tree, orient=tkinter.HORIZONTAL, command=tv_cards.xview)
scrollbar.pack(fill=X, side=BOTTOM)

tv_cards.configure(xscrollcommand=scrollbar.set)
frame_tree.pack(side=RIGHT, anchor=N)
frame_top.pack(fill=X)

# Нижний контейнер
frame_bottom = ttk.Frame(frame_main, padding=5)

btn_add_card = ttk.Button(frame_bottom, padding=5, text='Добавить запись', command=addCard)
btn_add_card.grid(row=0, column=0, columnspan=2)

btn_del_card = ttk.Button(frame_bottom, padding=5, text='Удалить запись', command=delCard)
btn_del_card.grid(row=0, column=2, columnspan=2)

frame_bottom.pack(side=LEFT, fill=X)

frame_main.pack(fill=Y)

update()
page.mainloop()