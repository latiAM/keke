from time import sleep
from turtle import back
from celery import Celery

celery_app = Celery(__name__,back)

@celery_app.task
def reverse_task(text):
    sleep(60)
    return text[::-1]
    