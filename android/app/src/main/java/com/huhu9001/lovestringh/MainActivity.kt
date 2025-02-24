package com.huhu9001.lovestringh

import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController

class MainActivity : androidx.appcompat.app.AppCompatActivity() {
    override fun onCreate(savedInstanceState: android.os.Bundle?) {
        super.onCreate(savedInstanceState)

        val body = com.huhu9001.lovestringh.databinding.ActivityMainBinding.inflate(layoutInflater)
        setContentView(body.root)

        val navController = findNavController(R.id.nav_host_fragment_activity_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        setupActionBarWithNavController(navController, AppBarConfiguration(
            setOf(R.id.navigation_home, R.id.navigation_translit, R.id.navigation_regex)
        ))
        body.navView.setupWithNavController(navController)
    }
}