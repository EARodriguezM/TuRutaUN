import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:tu_ruta_un/providers/InAsyncCall.dart';
import 'package:tu_ruta_un/routes.dart';
import 'package:tu_ruta_un/screens/welcome/welcome_screen.dart';
import 'package:tu_ruta_un/theme.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => InAsyncCall()),
      ],
      child: const TuRutaUN(),
    ),
  );
}

class TuRutaUN extends StatelessWidget {
  const TuRutaUN({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Tu Ruta UN',
      theme: theme(),
      //home: WelcomeScreen(),
      initialRoute: WelcomeScreen.routeName,
      //routes: routes,
      onGenerateRoute: RoutesGenerator.generateRoute,
    );
  }
}
