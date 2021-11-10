import 'package:tu_ruta_un/constants.dart';
import 'package:tu_ruta_un/size_config.dart';
import 'package:flutter/material.dart';

class Body extends StatelessWidget {
  const Body({
    Key? key,
    required this.imagePath,
    required this.titleText,
    required this.bodyText,
  }) : super(key: key);

  final String imagePath;
  final String titleText;
  final String bodyText;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        SizedBox(height: SizeConfig.screenHeight * 0.2),
        Image.asset(
          imagePath,
          height: SizeConfig.screenHeight * 0.4,
        ),
        SizedBox(height: SizeConfig.screenHeight * 0.08),
        Text(
          titleText,
          style: TextStyle(
            fontSize: getProportionateScreenWidth(30),
            fontWeight: FontWeight.bold,
            color: kTextSecondaryColor,
          ),
        ),
        Spacer(
          flex: 1,
        ),
        Text(
          bodyText,
          style: TextStyle(
            fontSize: getProportionateScreenWidth(15),
            color: kTextTertiaryColor,
          ),
        ),
        Spacer(
          flex: 2,
        ),
      ],
    );
  }
}
