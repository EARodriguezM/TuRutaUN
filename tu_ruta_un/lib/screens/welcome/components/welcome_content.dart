import 'package:tu_ruta_un/constants.dart';
import 'package:tu_ruta_un/size_config.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class WelcomeContent extends StatelessWidget {
  const WelcomeContent({
    Key? key,
    this.text,
    this.image,
  }) : super(key: key);
  final String? text, image;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        SizedBox(height: getProportionateScreenHeight(50)),
        Text(
          'Tu Ruta UN',
          style: TextStyle(
            fontSize: getProportionateScreenWidth(36),
            color: kTextPrimaryColor,
            fontWeight: FontWeight.bold,
          ),
        ),
        SizedBox(height: getProportionateScreenHeight(25)),
        Text(
          text!,
          textAlign: TextAlign.center,
          style: TextStyle(
            fontWeight: FontWeight.bold,
          ),
        ),
        Spacer(flex: 2),
        Image.asset(
          image!,
          width: image == 'assets/images/welcome_1.png'
              ? getProportionateScreenWidth(250)
              : getProportionateScreenWidth(325),
        )
      ],
    );
  }
}
