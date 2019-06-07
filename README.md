# Load balancer in .NET

This project objective was to create simple load  balancer. It has got capabilities of distributong load with several algorithms:

- round robin
- random
- connection count

Project is split up to two folders.

## Service

Simple site written in Python Flash to show number of actual instance. It is running on docker. Project contains also PowerShell script to run instances of this service.

## Load Balancer

[![Build Status](https://travis-ci.org/michalchecinski/load-balancer.svg?branch=master)](https://travis-ci.org/michalchecinski/load-balancer)

This is the main project folder. It contains Two ASP.NET Projects:

### LoadBalancer

[![LoadBalancer Build Status](https://travis-matrix-badges.herokuapp.com/repos/michalchecinski/load-balancer/branches/master/1)](https://travis-ci.org/michalchecinski/load-balancer)

Actual load balancer

### LoadBalancer.Logs.Web

[![LoadBalancer.Logs.Web Build Status](https://travis-matrix-badges.herokuapp.com/repos/michalchecinski/load-balancer/branches/master/2)](https://travis-ci.org/michalchecinski/load-balancer)

ASP.NET Core MVC project for presenting logs, metrics and charts of those for different time periods.
