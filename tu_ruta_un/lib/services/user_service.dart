import 'package:tu_ruta_un/status_code.dart';

import 'api_ip.dart';
import 'package:tu_ruta_un/models/user.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

String controllerURL = ApiIp.serverIP + '/User';

class UserService {
  Future<User> authentication(User authenticationRequest) async {
    var url = Uri.parse(controllerURL + '/authenticate');

    final authenticationResponse = await http.post(url,
        body: authenticationRequest.authenticateRequestToJson());
    var status = StatusCode(authenticationResponse.statusCode);

    if (status.statusCode == 200) {
      return User.authenticateResponseFromJSon(
          json.decode(authenticationResponse.body));
    }

    if (status.statusCode == 400) {
      authenticationRequest.token = '';
      return authenticationRequest;
    }

    throw Exception(status.statusDescription);
  }

  Future<String> register(User registerRequest) async {
    var url = Uri.parse(controllerURL + '/authentication');

    final registerResponse =
        await http.post(url, body: registerRequest.registerRequestToJson());
    var status = StatusCode(registerResponse.statusCode);

    if (status.statusCode == 200) {
      return 'Registered';
    } else {
      throw Exception(status.statusDescription);
    }
  }
}
