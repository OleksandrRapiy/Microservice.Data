# API Resources
This is public API for testing and everything what u want.


### Heroku deployment


```cmd
docker tag <image-name> registry.heroku.com/microservice-data-api/web


docker push registry.heroku.com/microservice-data-api/web


heroku container:release web --app microservice-data-api
```