# BasketAPI

API representing a shopping basket system.

## How to run locally

    git clone git@github.com:orangudan/BasketAPI.git
    cd BasketAPI
    docker build -t basketapi .
    docker run --rm -it -e "Auth:TokenSecret=asdflkjhasdflkjh" -p 4000:80 basketapi
    
The service will then be available to use at [http://localhost:4000](http://localhost:4000)

## Hosted instance

#### API base address

http://dockerbasketapi.southcentralus.cloudapp.azure.com/

#### Documentation:

http://dockerbasketapi.southcentralus.cloudapp.azure.com/swagger/index.html
