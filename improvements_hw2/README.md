
![homework content](https://raw.githubusercontent.com/tugaysarici/patika_dotnet_weekly_assignments/main/restfulapi_hw1/homework_1.png "Homework Content") 
# Auto Gallery

I developed RESTFUL APIs for an auto gallery example.



## API Usage

#### Get all records in the database:

```http
GET /api/Autos
```
```requesturl
https://localhost:44301/api/Autos
```
```json
[
  {
    "id": 1,
    "status": true,
    "brand": "Toyota",
    "model": "Corolla",
    "modelYear": 2016,
    "recordDate": "2023-05-05T00:00:00",
    "soldDate": null,
    "price": 680000
  },
  {
    "id": 2,
    "status": false,
    "brand": "Ford",
    "model": "Focus",
    "modelYear": 2017,
    "recordDate": "2023-07-06T00:00:00",
    "soldDate": "2023-08-17T00:00:00",
    "price": 765000
  },
  {
    "id": 4,
    "status": true,
    "brand": "Citroen",
    "model": "C3 Cross",
    "modelYear": 2014,
    "recordDate": "2023-07-16T00:00:00",
    "soldDate": null,
    "price": 660000
  },
  {
    "id": 5,
    "status": true,
    "brand": "Volvo",
    "model": "S90",
    "modelYear": 2020,
    "recordDate": "2023-08-30T00:00:00",
    "soldDate": null,
    "price": 1860000
  },
  {
    "id": 6,
    "status": true,
    "brand": "Ford",
    "model": "Tourneo Connect",
    "modelYear": 2013,
    "recordDate": "2023-09-17T00:00:00",
    "soldDate": null,
    "price": 600000
  },
  {
    "id": 7,
    "status": false,
    "brand": "Tofaş",
    "model": "Doğan",
    "modelYear": 1995,
    "recordDate": "2023-09-10T00:00:00",
    "soldDate": "2023-09-14T00:00:00",
    "price": 260000
  },
  {
    "id": 8,
    "status": true,
    "brand": "Audi",
    "model": "A4 Avant",
    "modelYear": 2022,
    "recordDate": "2023-05-15T00:00:00",
    "soldDate": null,
    "price": 1700000
  }
]
```


#### Get the record specified with id:

```http
GET /api/Autos/{id}
```
```requesturl
https://localhost:44301/api/Autos/5
```
```json
{
  "id": 5,
  "status": true,
  "brand": "Volvo",
  "model": "S90",
  "modelYear": 2020,
  "recordDate": "2023-08-30T00:00:00",
  "soldDate": null,
  "price": 1860000
}
```


#### Create a new record:

```http
POST /api/Autos
```
```requesturl
https://localhost:44301/api/Autos

{
  "status": true,
  "brand": "Seat",
  "model": "Ibiza",
  "modelYear": 2017,
  "recordDate": "2023-09-23T11:25:42.993Z",
  "price": 940000
}
```
```json
Recorded successfully.
```


#### Update the record specified with id:

```http
PUT /api/Autos/{id}
```
```requesturl
https://localhost:44301/api/Autos/8?price=1500000
The price of the record with id = 8 has been updated from 1700000 to 1500000.
{
  "id": 8,
  "status": true,
  "brand": "Audi",
  "model": "A4 Avant",
  "modelYear": 2022,
  "recordDate": "2023-05-15T00:00:00",
  "soldDate": null,
  "price": 1500000
}
```
```json
Record updated successfully.
```


#### Delete the record specified with id:

```http
DELETE /api/Autos/{id}
```
```requesturl
https://localhost:44301/api/Autos/18
The record with id = 8 has been deleted.
```
```json
Record deleted successfully.
```