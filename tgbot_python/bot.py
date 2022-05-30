import telebot
import datetime
from os import environ

bot = telebot.TeleBot('5321621829:AAF6ecnQKCMfNLsLKUY-8N40O8ONcg6JvbU')
global a1, a2
@bot.message_handler(commands=['hello'])


def get_text_messages(message):
    bot.send_message(message.from_user.id, 
    "Привет "+message.from_user.first_name)

@bot.message_handler(commands=['help'])
def get_text_messages(message):
    bot.send_message(message.from_user.id, "Напиши команду")

@bot.message_handler(commands=['time'])
def get_text_messages(message):
    months = ["января","февраля","марта","апреля","мая","июня",
    "июля","августа","сентября","октября","ноября","декабря"]
    now = datetime.datetime.now()
    bot.send_message(message.from_user.id, f"Сейчас {now.hour}:{now.minute}, {now.day} {months[now.month-1]} {now.year} года")

@bot.message_handler(commands=['binom'])
def handle_text(message):
    bot.send_message(message.from_user.id, "Для расчета биномиального коэффициента для N по K введите 2 числа через пробел\n(где первое число это N, а второе K)")
    @bot.message_handler(content_types=['text'])
    def handle_text(message):
        nums = message.text.split()
        def C(m,n):
            if m == 0 or m == n:
                return 1
            else:
                return C(m, n - 1) + C(m - 1, n - 1)
        bot.send_message(message.from_user.id, C(int(nums[0]), int(nums[1]))) 
@bot.message_handler(commands=['cat'])
def handle_text(message):
    bot.send_message(message.from_user.id,"")


bot.polling(none_stop=True, interval=0)