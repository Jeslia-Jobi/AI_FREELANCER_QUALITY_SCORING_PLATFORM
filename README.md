# AI Freelancer Quality Scoring Platform

## Project Overview

The **AI Freelancer Quality Scoring Platform** is a full-stack web application that helps clients identify suitable freelancers by evaluating their performance using project history, client reviews, ratings, and AI-based sentiment analysis.

The platform provides secure authentication for two user roles: **Clients** and **Freelancers**.

Clients can create projects, manage assigned work, submit reviews, and receive freelancer recommendations. Freelancers can maintain their profiles, browse available projects, apply for projects, track completed work, and view their overall quality score.

An AI module built using *ML.NET* analyzes review text submitted by clients to determine its sentiment. The sentiment score is combined with freelancer ratings and completed projects to calculate an overall quality score that supports client decision-making.

---

## Tech Stack Used

### Frontend

* Angular
* TypeScript
* HTML
* CSS

### Backend

* ASP.NET Core Web API
* C#

### Database

* SQLite
* Entity Framework Core

### AI / Machine Learning

* ML.NET
* Sentiment Analysis

---

## Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/Jeslia-Jobi/AI_Freelancer_Quality_Scoring_Platform.git
```

```bash
cd AI_Freelancer_Quality_Scoring_Platform
```

### 2. Backend Setup

```bash
cd api/FreelancerAPI
dotnet restore
dotnet ef database update
```

### 3. Frontend Setup

Open another terminal.

```bash
cd ui/freelancer-frontend
npm install
```

---

## How to Run API

Navigate to the API project.

```bash
cd api/FreelancerAPI
```

Run the application.

```bash
dotnet run
```

The API will be available on the localhost URL shown in the terminal. Swagger can be accessed by appending `/swagger` to the API URL.

---

## How to Run UI

Navigate to the frontend project.

```bash
cd ui/freeancer-frontend
```

Start the Angular development server.

```bash
ng serve
```

Open your browser and visit:

```
http://localhost:4200
```

---

## How to Run Tests

### Backend Tests

```bash
cd api/FreelancerAPI.Tests
dotnet test
```

### Frontend Tests

```bash
cd ui/freelancer-frontend
ng test
```

---
