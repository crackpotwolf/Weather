# Запись и получение погодных данных 
Данный сервис доступен по адресу https://weathervolgograd.herokuapp.com/weather. Он каждую минуту получает данные от https://openweathermap.org/, и если это новая запись, то сохраняет данные. К сожаленью Heroku через определённое время останавливает работу сервиса, а так как данные хранятся в SQLite то они не сохраняются после перезапуска сервиса. Так же у сервиса есть API доступное по ссылке https://weathervolgograd.herokuapp.com/swagger/index.html.

# Пример 
![](https://sun9-64.userapi.com/q5-Z6h0j3JVqSawvUY84ekjjWKtiUSAbTovwfQ/1SuhKGRFGl0.jpg)
