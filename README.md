# CKO Payment Gateway
This repository is a submission for the coding challenge at Checkout.com

The API projects contains implementation for two 

# How to
## Prerequisite
The project is built targeting .NET 6


### Restore Nuget Packages
In the solution directory, spin up a terminal and type `dotnet restore` to restore the project dependencies for the entire solution

### Run the API project

- Navigate to the API project directory `cd CheckOutPaymentGateway.API/`


## Obtain Auth Key
The project uses the Auth0 service to manage authentication concerns. So you'll need an access token to run the api endpoints.

See instructions below on how to obtain an API key using the credentials provided at time of task submission.


```bash
curl --request POST \
  --url https://ehimex-dev.auth0.com/oauth/token \
  --header 'content-type: application/json' \
  --data '{"client_id":"CLIENT_ID","client_secret":"CLIENT_SECRET","audience":"cko-gateway","grant_type":"client_credentials"}'

```

you'll receive a response like

```
{
  "access_token": "THE_ACCESS_TOKEN",
  "token_type": "Bearer"
}
```

You should extract the `access_token` property from the response for use in the API request

## Run the Project

- navigate into the API project directory by `cd CheckOutPaymentGateway.API`
- run `dotnet run --launch-profile API`

## Run tests

To run tests
1. Navigate to the solution directory
2. fill in the Auth API test credentials in the `CheckoutPaymentGateway.Tests.API/appsettings.json` file


## Call API Endpoints

To call the API endpoints, The access token retrieved above should be passed as a bearer token in your request Authorisation headers

When run locally, the API is available on

`https://localhost:5001/api/payment`

There are 2 API endpoints

### [GET] Retrieve payment transaction details


```bash
curl --request GET \
  --url https://localhost:5001/api/payment\
  --header 'authorization: Bearer THE_ACCESS_TOKEN'
```

### [POST] Process Payment Transaction

```bash
curl --request POST \
  --url https://localhost:5001/api/payment\
  --header 'authorization: Bearer THE_ACCESS_TOKEN
  -- data '{   "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "merchantId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cardHolderFullName": "string", "cardNumber": "string", "cardExpiryDate": "string", "cardCVV": "string",  "amount": 0,
  "currency": "string" }'
```


Alternatively, you can use the Swagger UI endpoint to test run the endpoints https://localhost:5001/index.html

# Bonus Points


## Authentication
The API endpoints are secured from unauthorised access. Only users with an access token are allowed to submit / retrieve a transaction. 


## Documentation

The API endpoint has a documentation page at https://localhost:5001/index.html. This page hosts the API endpoints doucmentation and it gives user a chance to test run the endpoints.
The source code contains illustrative comments that explains intention at the time of wrting. They also serve as a letter to my future self.

## Testing
The application cource has essential tests that cover the critical functionalities.

The API endpoints are covered by Integration tests which hints us that the basic usecases work as expected
Other core functionality int he service layer have unit test coverage

# Assumptions

The following assumptions were made in order to have a more reduced scope of implementation
- Bank cards will always be 16 digits and contain no space or hyphens
- CVV will always be 3 digits
- The Bank API will always process and return response in a reasonable amount of time

# Other Considerations
With more time resource, I can have more scope to cover other concerns

- Better test coverage. The current tests are a happy path testing and there's no test for many possible failure scenario.
    - No tests for
    - invalid data
    - No validation of transaction amount around extremes of zero and negative values.
    
Implement a circuit breaker around the connectivity to the bank API. This will ensure we deliver responses to clients in a reasonable amount of time, or timeout where neccesary.
 
