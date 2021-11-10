// import 'package:buen_doctor_app/screens/sign_in/sign_in_screen.dart';
// import 'package:buen_doctor_app/screens/sign_in_state/sign_in_state_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

import 'package:tu_ruta_un/screens/error/error_screen.dart';
import 'package:tu_ruta_un/screens/sign_in/sign_in_screen.dart';
import 'package:tu_ruta_un/screens/sign_in_state/sign_in_state_screen.dart';
import 'package:tu_ruta_un/screens/welcome/welcome_screen.dart';

// final Map<String, WidgetBuilder> routes = {
//   WelcomeScreen.routeName: (context) => WelcomeScreen(),
//   SignInScreen.routeName: (context) => SignInScreen(),
//   SignInStateScreen.routeName: (context) => SignInStateScreen(),
// };

class RoutesGenerator {
  static Route<dynamic> generateRoute(RouteSettings routeSettings) {
    final args = routeSettings.arguments;

    if (routeSettings.name == WelcomeScreen.routeName) {
      return MaterialPageRoute(builder: (_) => WelcomeScreen());
    }

    if (routeSettings.name == SignInScreen.routeName) {
      return MaterialPageRoute(builder: (_) => SignInScreen());
    }

    if (routeSettings.name == SignInStateScreen.routeName) {
      if (args is bool) {
        return MaterialPageRoute(builder: (_) => SignInStateScreen(isOk: args));
      }
    }

    //   // if(routeSettings.name == .routeName){

    //   // }

    //   // if(routeSettings.name == .routeName){

    //   // }

    //   // if(routeSettings.name == .routeName){

    //   // }

    return MaterialPageRoute(builder: (_) => ErrorScreen());
  }
}
