import telebot
import datetime

bot = telebot.TeleBot('5321621829:AAF6ecnQKCMfNLsLKUY-8N40O8ONcg6JvbU')
global a1, a2
@bot.message_handler(content_types=['text'])
def get_text_messages(message):
    if message.text == "/hello":
        bot.send_message(message.from_user.id, "Привет "+message.from_user.first_name)
    elif message.text == "/help":
        bot.send_message(message.from_user.id, "Напиши Привет")
    elif message.text == "/time":
        months = ["января","февраля","марта","апреля","мая","июня","июля","августа","сентября","октября","ноября","декабря"]
        now = datetime.datetime.now()
        bot.send_message(message.from_user.id, f"Сейчас {now.hour}:{now.minute}, {now.day} {months[now.month-1]} {now.year} года")
    elif message.text == "/binom {a1} {a2}":
        #bot.send_message(message.from_user.id, "Для расчета биномиального коэффициента для N по K введите 2 числа через пробел\n(где первое число это N, а второе K)")
        nums = message.text.split()
        try:
            int(nums[1], nums[2]) #проверяем, что возраст введен корректно
        except Exception:
            bot.send_message(message.from_user.id, 'Цифрами, пожалуйста')
        def C(m,n):
            if m == 0 or m == n:
                return 1
            else:
                return C(m, n - 1) + C(m - 1, n - 1)
        bot.send_message(message.from_user.id, C(nums[0], nums[1]))      
    else:
        bot.send_message(message.from_user.id, "Я тебя не понимаю. Напиши /help.")


bot.polling(none_stop=True, interval=0)
