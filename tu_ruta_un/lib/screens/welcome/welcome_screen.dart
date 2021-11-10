import 'package:tu_ruta_un/size_config.dart';
import 'package:flutter/material.dart';
import 'package:tu_ruta_un/screens/welcome/components/body.dart';

class WelcomeScreen extends StatelessWidget {
  static String routeName = '/welcome';
  @override
  Widget build(BuildContext context) {
    // initialize the class in the starting screen
    SizeConfig().init(context);
    return Scaffold(
      body: Body(),
    );
  }
}
