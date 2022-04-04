from flask import Flsak, render_template, session
app = Flask(__name__)
def index_page():
    print(session['tasks'])
    if 'tasks' not in session:
        session['tasks'] = []
    return render_template('index.html')

@flask_app.route('/revers/')
def reverse_page(task_id):
    task = reverse_task.AsyncResult('text')
    return render_template('result.html')
    
    
@flask_app.route('/revers/')
def reverse_page():
    session['tasks'].append({
        'id': task.id
    })
    return redirect('/')