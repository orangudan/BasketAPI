# BasketAPI

API representing a shopping basket system.

## How to run locally

    git clone git@github.com:orangudan/BasketAPI.git
    cd BasketAPI
    docker build -t basketapi .
    docker run --rm -it -e "Auth:TokenSecret=asdflkjhasdflkjh" -p 4000:80 basketapi

## Hosted instance

#### API base address

http://dckrbasketapi.centralus.cloudapp.azure.com/

#### Documentation:

http://dckrbasketapi.centralus.cloudapp.azure.com/swagger/index.html
