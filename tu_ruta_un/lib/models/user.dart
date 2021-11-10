import 'dart:convert';

import 'package:flutter/material.dart';

import 'user_type.dart';

class User {
  late final String userId;
  late final String username;
  late final String firstName;
  late final String? secondName;
  late final String firstSurname;
  late final String secondSurname;
  late final String password;
  late final String email;
  late final String mobile;
  late final List<int>? profilePicture;
  late final bool status;
  late final int userTypeId;
  late final String token;

  late final UserType userType;

  User(
      {required this.userId,
      required this.firstName,
      required this.secondName,
      required this.firstSurname,
      required this.secondSurname,
      required this.email,
      required this.mobile,
      required this.profilePicture});

  User.authenticateRequest({required this.username, required this.password});

  Map<String, dynamic> authenticateRequestToJson() {
    Map<String, dynamic> request = {
      'username': username.trim(),
      'password': password.trim(),
    };

    return request;
  }

  User.authenticateResponse(
      {required this.userId,
      required this.firstName,
      this.secondName,
      required this.firstSurname,
      required this.secondSurname,
      required this.email,
      required this.mobile,
      this.profilePicture,
      required this.token});

  factory User.authenticateResponseFromJSon(Map<String, dynamic> response) {
    List<int> bytesList = [];
    if (response['profilePicture'] != '') {
      var bytesString = response['profilePicture'] as String;
      bytesList = base64.decode(bytesString);
    }
    return User.authenticateResponse(
        userId: response['userId'] ?? '',
        firstName: response['firstName'] ?? '',
        secondName: response['secondName'] ?? '',
        firstSurname: response['firstSurname'] ?? '',
        secondSurname: response['secondSurname'] ?? '',
        email: response['email'] ?? '',
        mobile: response['mobile'] ?? '',
        profilePicture: bytesList,
        token: response['token'] ?? '');
  }

  User.registerRequest(
      {required this.userId,
      required this.firstName,
      this.secondName,
      required this.firstSurname,
      required this.secondSurname,
      required this.password,
      required this.email,
      required this.mobile,
      required this.userTypeId,
      this.profilePicture});

  Map<String, dynamic> registerRequestToJson() {
    Map<String, dynamic> request = {
      'userId': userId.trim(),
      'firstName': firstName.trim(),
      'secondName': secondName!.trim(),
      'firstSurname': firstSurname.trim(),
      'secondSurname': secondSurname.trim(),
      'mobile': mobile.trim(),
      'userTypeId': userTypeId,
      'profilePicture': profilePicture,
    };

    return request;
  }
}
