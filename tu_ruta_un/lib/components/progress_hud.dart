import 'package:tu_ruta_un/constants.dart';
import 'package:flutter/material.dart';

class ProgressHUD extends StatelessWidget {
  final Widget child;
  final bool inAsyncCall;
  final double opacity;
  final Color color;
  final Animation<Color>? valueColor;

  ProgressHUD({
    Key? key,
    required this.child,
    this.inAsyncCall = false,
    this.opacity = 0.4,
    this.color = kPrimaryColor,
    this.valueColor,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    List<Widget> widgets = [];
    widgets.add(child);
    if (inAsyncCall) {
      final stack = new Stack(
        children: [
          new Opacity(
            opacity: opacity,
            child: ModalBarrier(
              dismissible: false,
              color: color,
            ),
          ),
          new Center(
            child: new CircularProgressIndicator(),
          ),
        ],
      );
      widgets.add(stack);
    }
    return Stack(
      children: widgets,
    );
  }
}
