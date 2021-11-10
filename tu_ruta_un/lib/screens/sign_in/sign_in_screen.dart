import 'package:tu_ruta_un/components/progress_hud.dart';
import 'package:tu_ruta_un/constants.dart';
import 'package:tu_ruta_un/providers/InAsyncCall.dart';
import 'package:tu_ruta_un/screens/sign_in/components/body.dart';
import 'package:flutter/material.dart';
import 'package:flutter/foundation.dart';
import 'package:tu_ruta_un/size_config.dart';
import 'package:provider/src/provider.dart';

class SignInScreen extends StatelessWidget {
  static String routeName = '/';
  const SignInScreen({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    // initialize the class in the starting screen
    SizeConfig().init(context);
    return ProgressHUD(
      child: Scaffold(
        backgroundColor: kSecondaryColor,
        body: Body(),
      ),
      inAsyncCall: context.watch<InAsyncCall>().inAsyncCall,
    );
  }
}
