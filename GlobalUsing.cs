global using NewMVCProjekt.Data;
global using NewMVCProjekt;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Http.Headers;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using NewMVCProjekt.Models;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Claims;
global using System.Runtime.InteropServices;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using System.IdentityModel.Tokens.Jwt;
global using System.IdentityModel.Tokens;
global using System.Text;
global using System.ComponentModel.DataAnnotations.Schema;
global using System;
global using BCrypt.Net;
global using Microsoft.IdentityModel.Tokens;
global using NewMVCProjekt.Types;
global using NewMVCProjekt.Servises;
global using NewMVCProjekt.Middleware;
global using Microsoft.Extensions.Logging;
global using Karambolo.Extensions.Logging.File;
global  using Microsoft.AspNetCore.NodeServices;





//var claims = new List<Claim>
// {


// new Claim (JwtRegisteredClaimNames.Name,user.FirstName),
// new Claim (JwtRegisteredClaimNames.Email,user.Email)
//  };

// byte[] secretBytes = Encoding.UTF8.GetBytes(Consts.SecretKey);
// var key = new SymmetricSecurityKey(secretBytes);

// var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

// var token = new JwtSecurityToken(
//     issuer: Consts.issuer,
//     audience: Consts.audience,
//     claims: claims,
//    notBefore: DateTime.Now,
//    expires: DateTime.Now.AddMinutes(30),
//    signingCredentials


//   )





//var response = new
//{
//  access_token = Usertoken,
//  username = user.Email
//};

//ViewBag.Token = response;
//Response.Headers.Add("Authorization", "Bearer " + Usertoken);



//string authorizationHeader = "Bearer " + Usertoken;
//string redirectUrl = "https://localhost:7182/Authentication/Useracount";

//Створення об'єкту HttpClient
// var httpClient = new HttpClient();

// Додавання заголовка Authorization до запиту
//httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

// Виконання GET-запиту
//var response = httpClient.GetAsync(redirectUrl).Result;

// Повернення відповіді сервера
//return Content(response.Content.ReadAsStringAsync().Result, "text/html");

// HttpContext.Request.Headers.Add("Authorization", authorizationHeader);
//return RedirectToAction("Useracount");