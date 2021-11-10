import 'dart:async';

import 'package:tu_ruta_un/models/user.dart';
import 'package:tu_ruta_un/screens/sign_in/sign_in_screen.dart';
import 'package:tu_ruta_un/screens/sign_in_state/components/body.dart';
import 'package:flutter/material.dart';

class SignInStateScreen extends StatelessWidget {
  static String routeName = "/sign_in_state";
  final bool isOk;

  const SignInStateScreen({
    Key? key,
    this.isOk = false,
  }) : super(key: key);
  @override
  Widget build(BuildContext context) {
    String imagePath = '';
    String labelText = '';
    String bodyText = '';

    FutureOr<Null> Function()? routeToTake;

    if (isOk) {
      imagePath = 'assets/images/success.png';
      labelText = 'Acceso Permitido';
      bodyText = '';
      routeToTake = () {
        print('ñaca ñaca');
      };
    } else {
      imagePath = 'assets/images/fail.png';
      labelText = 'Acceso Denegado';
      bodyText = 'Contraseña o E-Mail incorrectos, verifique.';
      routeToTake = () {
        Navigator.pop(context);
        // Navigator.popUntil(
        //     context, ModalRoute.withName(SignInScreen.routeName));
      };
    }

    Future.delayed(const Duration(seconds: 3), routeToTake);

    return Scaffold(
      body: Body(
        imagePath: imagePath,
        titleText: labelText,
        bodyText: bodyText,
      ),
    );
  }
}

// class SignInStateScreen extends StatefulWidget {
//   static String routeName = "/sign_in_state";
//   final bool isOk;

//   const SignInStateScreen({Key? key, required this.isOk}) : super(key: key);
//   @override
//   _SignInStateScreenState createState() => _SignInStateScreenState();
// }

// class _SignInStateScreenState extends State<SignInStateScreen> {
//   _SignInStateScreenState(
//     bool _isOk,
//   );
//   @override
//   void initState() {
//     super.initState();
//     Timer(
//         Duration(seconds: 3),
//         isOk
//             ? () => print('ÑACA ÑACA')
//             : () => Navigator.popUntil(
//                 context, ModalRoute.withName(SignInScreen.routeName)));
//   }

//   @override
//   Widget build(BuildContext context) {
//     String imagePath = '';
//     String labelText = '';
//     String bodyText = '';

//     if (isOk) {
//       imagePath = 'assets/images/success.png';
//       labelText = 'Acceso Permitido';
//       bodyText = '';
//     } else {
//       imagePath = 'assets/images/fail.png';
//       labelText = 'Acceso Denegado';
//       bodyText = 'Contraseña o E-Mail incorrectos, verifique.';
//     }

//     return Scaffold(
//       backgroundColor: Colors.black,
//       body: Body(
//         imagePath: imagePath,
//         titleText: labelText,
//         bodyText: bodyText,
//       ),
//     );
//   }
// }
