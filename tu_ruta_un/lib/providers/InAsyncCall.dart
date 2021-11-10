import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class InAsyncCall with ChangeNotifier, DiagnosticableTreeMixin {
  bool _inAsyncCall = false;

  bool get inAsyncCall => _inAsyncCall;

  void initAsyncCall() {
    _inAsyncCall = true;
    notifyListeners();
  }

  void finalizeAsynCall() {
    _inAsyncCall = false;
    notifyListeners();
  }
}
