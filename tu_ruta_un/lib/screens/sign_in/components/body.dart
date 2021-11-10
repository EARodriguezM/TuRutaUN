import 'package:tu_ruta_un/constants.dart';
import 'package:tu_ruta_un/screens/sign_in/components/sign_in_form.dart';
import 'package:flutter/material.dart';
import 'package:tu_ruta_un/size_config.dart';

class Body extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: SizedBox(
        width: double.infinity,
        child: Padding(
          padding:
              EdgeInsets.symmetric(horizontal: getProportionateScreenWidth(20)),
          child: SingleChildScrollView(
            child: Column(
              children: [
                SizedBox(height: SizeConfig.screenHeight * 0.04),
                Text(
                  "Bienvenido",
                  style: TextStyle(
                    color: kTextSecondaryColor,
                    fontSize: getProportionateScreenWidth(38),
                    fontWeight: FontWeight.bold,
                  ),
                ),
                SizedBox(height: SizeConfig.screenHeight * 0.05),
                const Text(
                  'Ingrese con su correo institucional y \nsu contraseña para acceder a las funcionalidades',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    color: kTextTertiaryColor,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                Container(
                    width: 300.0,
                    height: 300.0,
                    decoration: const BoxDecoration(
                        shape: BoxShape.circle,
                        image: DecorationImage(
                            fit: BoxFit.fill,
                            image: AssetImage(
                                'assets/images/login_picture.png')))),
                SignInForm(),
                SizedBox(height: SizeConfig.screenHeight * 0.05),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
