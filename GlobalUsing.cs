global using NewMVCProjekt.Data;
global using NewMVCProjekt;
global using MySql.Data.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Storage.Internal;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using NewMVCProjekt.Models;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Identity;
global using NewMVCProjekt.Traningclases;
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
