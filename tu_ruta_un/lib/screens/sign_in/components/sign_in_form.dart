import 'dart:async';

import 'package:flutter/cupertino.dart';
import 'package:tu_ruta_un/components/default_button.dart';
import 'package:tu_ruta_un/components/form_errors.dart';
import 'package:tu_ruta_un/components/surfix_icon.dart';
import 'package:tu_ruta_un/constants.dart';
import 'package:tu_ruta_un/helpers/keyboard.dart';
import 'package:tu_ruta_un/models/user.dart';
import 'package:tu_ruta_un/providers/InAsyncCall.dart';
import 'package:tu_ruta_un/screens/sign_in_state/sign_in_state_screen.dart';
import 'package:tu_ruta_un/services/user_service.dart';
import 'package:tu_ruta_un/size_config.dart';
import 'package:flutter/material.dart';
import 'package:provider/src/provider.dart';

class SignInForm extends StatefulWidget {
  @override
  _SignInFormState createState() => _SignInFormState();
}

class _SignInFormState extends State<SignInForm> {
  final _formKey = GlobalKey<FormState>();
  late String username;
  late String password;
  bool remember = false;

  final List<String> errors = [];

  void addError({required String error}) {
    if (!errors.contains(error)) {
      setState(() {
        errors.add(error);
      });
    }
  }

  void removeError({required String error}) {
    if (errors.contains(error)) {
      setState(() {
        errors.remove(error);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Form(
      key: _formKey,
      child: Column(
        children: [
          buildEmailFormField(),
          SizedBox(
            height: getProportionateScreenHeight(20),
          ),
          buildPasswordFormField(),
          SizedBox(height: getProportionateScreenHeight(20)),
          Row(
            children: [
              Checkbox(
                value: remember,
                activeColor: kPrimaryColor,
                onChanged: (value) {
                  setState(() {
                    remember = value!;
                  });
                },
              ),
              const Text("Recuerdame"),
              Spacer(),
              Column(
                children: [
                  GestureDetector(
                    //onTap: () => Navigator.pushNamed(
                    //context, ForgotPasswordScreen.routeName),
                    child: const Text(
                      "Ha olvidado su contraseña?",
                      style: TextStyle(decoration: TextDecoration.underline),
                    ),
                  ),
                  SizedBox(height: getProportionateScreenHeight(15)),
                  GestureDetector(
                    //onTap: () => Navigator.pushNamed(
                    //context, ForgotPasswordScreen.routeName),
                    child: const Text(
                      "Desea Registrarse?",
                      style: TextStyle(decoration: TextDecoration.underline),
                    ),
                  ),
                ],
              )
            ],
          ),
          SizedBox(height: getProportionateScreenHeight(10)),
          FormErrors(errors: errors),
          SizedBox(height: getProportionateScreenHeight(20)),
          DefaultButton(
            text: "Ingresar",
            press: () {
              if (_formKey.currentState!.validate()) {
                _formKey.currentState!.save();
                KeyboardUtil.hideKeyboard(context);
                signIn(context);
              }
            },
          ),
        ],
      ),
    );
  }

  signIn(BuildContext context) async {
    context.read<InAsyncCall>().initAsyncCall();
    User authenticationRequest =
        new User.authenticateRequest(username: username, password: password);
    UserService dataUserService = new UserService();

    try {
      await dataUserService
          .authentication(authenticationRequest)
          .then((authenticationResponse) => {
                context.read<InAsyncCall>().finalizeAsynCall(),
                if (authenticationResponse.runtimeType == User)
                  {
                    if (authenticationResponse.token.isNotEmpty)
                      {
                        Navigator.pushNamed(
                            context, SignInStateScreen.routeName,
                            arguments: true)
                        // Other way to pass parameters
                        // Navigator.push(
                        //   context,
                        //   MaterialPageRoute(
                        //     builder: (_) => SignInStateScreen(
                        //       isOk: true,
                        //     ),
                        //   ),
                        // )
                      }
                    else
                      {
                        Navigator.pushNamed(
                            context, SignInStateScreen.routeName,
                            arguments: false)
                      }
                  }
              });
    } catch (e) {
      context.read<InAsyncCall>().finalizeAsynCall();
      print(e);
    }
  }

  buildEmailFormField() {
    return TextFormField(
      keyboardType: TextInputType.emailAddress,
      onSaved: (newValue) => username = newValue!.toLowerCase(),
      onChanged: (value) {
        if (value.isNotEmpty) {
          removeError(error: kEmailNullError);
        }

        if (value.length >= 3) {
          removeError(error: kShortUsername);
        }

        if (!value.contains(' ')) {
          removeError(error: kSpaceInUsername);
        }

        return null;
      },
      validator: (value) {
        if (value!.isEmpty) {
          addError(error: kEmailNullError);
          return "";
        }
        if (value.length < 3) {
          addError(error: kShortUsername);
          return "";
        }
        if (value.contains(' ')) {
          addError(error: kSpaceInUsername);
          return "";
        }
        return null;
      },
      decoration: InputDecoration(
        labelText: 'Usuario',
        hintText: 'Ingrese su Usuario',
        floatingLabelBehavior: FloatingLabelBehavior.always,
        //suffixIcon: SurffixIcon(svgIcon: "assets/icons/Mail.svg"),
        suffixText: "@unal.edu.co",
      ),
    );
  }

  buildPasswordFormField() {
    return TextFormField(
      obscureText: true,
      onSaved: (newValue) => password = newValue!,
      onChanged: (value) {
        if (value.isNotEmpty) {
          removeError(error: kPassNullError);
        } else if (value.length >= 8) {
          removeError(error: kShortPassError);
        }
        return null;
      },
      validator: (value) {
        if (value!.isEmpty) {
          addError(error: kPassNullError);
          return "";
        } else if (value.length < 8) {
          addError(error: kShortPassError);
          return "";
        }
        return null;
      },
      decoration: InputDecoration(
        labelText: "Contraseña",
        hintText: "Ingrese su contraseña",
        floatingLabelBehavior: FloatingLabelBehavior.always,
        suffixIcon: SurffixIcon(svgIcon: "assets/icons/Lock.svg"),
      ),
    );
  }
}
